LibraryCriFs = {

$CriFs: {
	initialized: false,
	reqs: {},		// リクエスト中のXHRを保持する。キーはCriFsLoaderHn
	ins: {},		// 実行中のインストーラを保持する。キーはハンドル
	xhr: {
		headers: {},
		timeout: 0,
	},
	insMgr: {
		unique: 0,
		headers: {},
		curIns: 0,
		maxIns: 4,
		timeout: 0,
		crcTable: null,
	},
	
	init: function() {
		/**
		 * HttpRequest
		 * @constructor
		 */
		CriFs.XHR = function() {
		};
		CriFs.XHR.prototype.start = function(url, offset, size, onload, onerror) {
			var req = this.req = new XMLHttpRequest();
			this.data = null;
			this.offset = offset;
			this.size = size;
			this.onload = onload;
			this.onerror = onerror;
			this.range = false;
			
			// offset,sizeが0の時はHEADリクエスト
			req.open((offset == 0 && size == 0) ? 'HEAD' : 'GET', url, true);	
			req.responseType = "arraybuffer";
			
			// offset,sizeが有効なときは、Rangeリクエストを追加
			if (offset > 0 || (size > 0 && size < 0xFFFFFFFF)) {
				req.setRequestHeader("Range", "bytes=" + offset + "-" + (size + offset - 1));
				// Rangeアクセスしたときキャッシュが有効だとChromeで失敗することがあるので無効化
				req.setRequestHeader('Pragma', 'no-cache');
				req.setRequestHeader('Cache-Control', 'no-cache');
				req.setRequestHeader('If-Modified-Since', 'Thu, 01 Jun 1970 00:00:00 GMT');
				this.range = true;
			}

			// 追加ヘッダを指定
			var headers = CriFs.xhr.headers;
			for (var i in headers) {
				req.setRequestHeader(i, headers[i]);
			}

			// ロード完了
			req.onload = this._onload.bind(this);
			// 正常なレスポンスが得られなかった
			req.onerror = this._onerror.bind(this);
			// タイムアウト処理
			var timeout = CriFs.xhr.timeout;
			if (timeout > 0) {
				this.timeout = timeout;
				this.intervalID = setInterval(this._update.bind(this), 100);
			}
			this.progressTime = Date.now();
			req.addEventListener("progress", this._onprogress.bind(this));
			// リクエスト開始
			req.send();
		};
		CriFs.XHR.prototype.stop = function() {
			if (this.req) {
				this.req.abort();
				this.req = null;
			}
			if (this.intervalID) {
				clearInterval(this.intervalID);
				this.intervalID = null;
			}
		};
		CriFs.XHR.prototype.read = function(ptr, offset, size) {
			// データのコピー
			if (this.data) {
				size = Math.min(this.data.length - offset, size);
				if ((ptr&3)===0&&(offset&3)===0&&(size&3)===0) {
					HEAPU32.set(new Uint32Array(this.data.buffer, this.data.byteOffset + offset, size>>2), ptr>>2);
				} else {
					HEAP8.set(this.data.subarray(offset, offset + size), ptr);
				}
				return size;
			}
			return 0;
		};
		CriFs.XHR.prototype._onload = function() {
			var req = this.req;
			if (req.status == 200 || req.status == 206) {
				if (req.status == 200 && this.range) {
					// Rangeリクエストしたのに200で返すサーバー対策
					this.data = new Uint8Array(req.response, this.offset, this.size);
				} else {
					this.data = new Uint8Array(req.response)
				}
				this.onload(this.data.length);
			} else {
				this.onerror();
			}
			this.req = null;
			this.stop();
		};
		CriFs.XHR.prototype._onerror = function() {
			this.onerror();
			this.req = null;
			this.stop();
		};
		CriFs.XHR.prototype._onprogress = function(e) {
			this.status = this.req.status;
			if (e.lengthComputable) {
				this.progressTime = Date.now();
				if (this.onprogress) this.onprogress(e);
			}
		};
		CriFs.XHR.prototype._update = function() {
			if (Date.now() - this.progressTime >= this.timeout) {
				this.stop();
				if (this.ontimeout) this.ontimeout();
				else this.onerror();
			}
		};

		/**
		 * Memory FileRequest
		 * @constructor
		 */
		CriFs.MEM = function() {
		};
		CriFs.MEM.prototype.start = function(path, offset, size, onload, onerror) {
			if (path.indexOf("memfs:") == 0) path = path.slice(6);
			this.offset = offset;
			this.size = size;

			try {
				var stat = memfs.stat(path);
				this.stm = memfs.open(path, "r");
				var csize = stat.size - offset;
				if (size > 0 && size < 0xFFFFFFFF) {
					if (csize > size) csize = size;
				}
				onload(csize);
			} catch (e) {
				console.error("MEMFS: '" + path + "' not found.");
				onerror();
			}
		};
		CriFs.MEM.prototype.stop = function() {
			if (this.stm) memfs.close(this.stm);
		};
		CriFs.MEM.prototype.read = function(buffer, offset, size) {
			return (this.stm) ? memfs.read(this.stm, HEAP8, buffer, size, this.offset + offset) : 0;
		};

		CriFs.MEM.writeFile = function(path, data, oncomplete, onerror) {
			if (path.indexOf("memfs:") == 0) path = path.slice(6);
			if (CriFs.MEM.existsFile(path)) {
				if (onerror) onerror();
				return;
			}
			try {
				var stm = memfs.open(path, "w+");
				memfs.write(stm, new Uint8Array(data), 0, data.byteLength);
				memfs.close(stm);
				memfs.syncfs(false, oncomplete);
			} catch (e) {
				if (onerror) onerror();
			}
		}
		CriFs.MEM.readFile = function(path, oncomplete, onerror) {
			if (path.indexOf("memfs:") == 0) path = path.slice(6);
			try {
				var stat = memfs.stat(path);
				var buffer = new Uint8Array(stat.size);
				var stm = memfs.open(path, "r");
				memfs.read(stm, buffer, 0, buffer.byteLength);
				memfs.close(stm);
				if (oncomplete) oncomplete(buffer);
			} catch (e) {
				if (onerror) onerror();
			}
		};
		CriFs.MEM.existsFile = function(path) {
			if (path.indexOf("memfs:") == 0) path = path.slice(6);
			try {
				var stat = memfs.stat(path);
				if (stat) return true;
			} catch (e) {
			}
			return false;
		};
		CriFs.MEM.removeFile = function(path, oncomplete, onerror) {
			if (path.indexOf("memfs:") == 0) path = path.slice(6);
			try {
				memfs.unlink(path);
				memfs.syncfs(false, oncomplete);
			} catch (e) {
				if (onerror) onerror();
			}
		};
		CriFs.MEM.renameFile = function(oldpath, newpath, oncomplete, onerror) {
			if (oldpath.indexOf("memfs:") == 0) oldpath = oldpath.slice(6);
			if (newpath.indexOf("memfs:") == 0) newpath = newpath.slice(6);
			try {
				memfs.rename(oldpath, newpath);
				memfs.syncfs(false, oncomplete);
			} catch (e) {
				if (onerror) onerror();
			}
		};

		/**
		 * IndexedDB FileRequest
		 * @constructor
		 */
		CriFs.IDB = function() {
		};
		CriFs.IDB.prototype.start = function(path, offset, size, onload, onerror) {
			if (path.indexOf("idbfs:") == 0) path = path.slice(6);
			this.offset = offset;
			this.size = size;
			this.info = null;
			this.entityData = null;
			
			var file = idbfs.files[path];
			if (file) {
				this.info = file;
				size = (size > 0 && size < 0xFFFFFFFF) ? size : file.size;
				this.buffer = new ArrayBuffer(size);
				this.tr = this._load(file, offset, size, function(entityData) {
					this.entityData = entityData;
					this.tr = null;
					onload(size);
				}.bind(this), onerror);
			} else {
				// ファイルが存在しない
				console.error("IDBFS: '" + path + "' not found.");
				onerror();
			}
		};
		CriFs.IDB.prototype.stop = function() {
			if (this.tr) this.tr.abort();
		};
		CriFs.IDB.prototype.read = function(buffer, offset, size) {
			var entities = this.info.entities;
			var entityData = this.entityData;
			var position = this.offset + offset;
			var remain = size;
			for (var i = 0; i < entities.length; i++) {
				var entity = entities[i];
				if (position + remain <= entity.offset) {
					break;
				}
				if (entity.offset + entity.length < position) {
					continue;
				}
				var data = entityData[i];
				var readofs = position - entity.offset;
				var readsize = (remain > entity.length - readofs) ? entity.length - readofs : remain;
				
				var ptr = buffer + position - this.offset;
				var target = HEAP8.subarray(ptr, ptr + readsize);
				target.set(data.subarray(readofs, readofs + readsize));

				position += readsize;
				remain -= readsize;
			}
		};
		CriFs.IDB.prototype._load = function(info, offset, size, oncomplete, onerror) {
			var tr = idbfs.db.transaction(["entities"], "readonly");
			var store = tr.objectStore("entities");
			
			var entities = info.entities;
			var entityData = new Array(entities.length);
			var errorNotified = false;

			function readEntity(key, index) {
				var req = store.get(key);
				req.onsuccess = function(e) {
					var data = req.result;
					//console.log(readofs, readsize, data.byteLength);
					if (data) {
						entityData[index] = data;
					} else if (!errorNotified) {
						onerror();
						errorNotified = true;
					}
				}
				req.onerror = onerror;
			}

			var position = offset;
			var remain = size;
			for (var i = 0; i < entities.length; i++) {
				var entity = entities[i];
				if (position + remain <= entity.offset) {
					break;
				}
				if (entity.offset + entity.length < position) {
					continue;
				}
				var readofs = position - entity.offset;
				var readsize = (remain > entity.length - readofs) ? entity.length - readofs : remain;
				readEntity(entity.key, i);
				position += readsize;
				remain -= readsize;
			}

			tr.oncomplete = function() {
				oncomplete(entityData);
			};
			tr.onerror = onerror;
			return tr;
		};

		var idbfs;
		CriFs.IDB.init = function(dbname, oncomplete, onerror) {
			if (idbfs) return;
			idbfs = {db:null, dbname: dbname, files:{}};
			var indexedDB = window.indexedDB || window.webkitIndexedDB;
			if (!indexedDB) return false;
			var dbreq = indexedDB.open(dbname);
			dbreq.onupgradeneeded = function() {
				idbfs.db = dbreq.result;
				idbfs.db.createObjectStore("toc", {keyPath:"path"});
				idbfs.db.createObjectStore("entities", {autoIncrement:true});
			};
			dbreq.onsuccess = function() {
				idbfs.db = dbreq.result;
				//console.log(idbfs.db.name, idbfs.db.version);
				//console.log(idbfs.db.objectStoreNames);

				var tr = idbfs.db.transaction("toc");
				var store = tr.objectStore("toc");
				if (store.getAll) {
					var req = store.getAll();
					req.onsuccess = function() {
						var files = req.result;
						for (var i in files) {
							var file = files[i];
							idbfs.files[file.path] = file;
						}
						if (oncomplete) oncomplete();
					};
				} else {
					var req = store.openCursor(null);
					req.onsuccess = function() {
						var cursor = req.result;
						if (cursor) {
							var file = cursor.value;
							idbfs.files[file.path] = file;
							cursor.continue();
						} else {
							if (oncomplete) oncomplete();
						}
					};
				}
				req.onerror = onerror;
			};
			dbreq.onerror = function() {
				idbfs = undefined;
				console.error("IDBFS initialization error.");
				if (onerror) onerror();
			};
		};
		CriFs.IDB.clear = function(oncomplete, onerror) {
			if (!idbfs) return;
			var indexedDB = window.indexedDB || window.webkitIndexedDB;
			if (!indexedDB) return false;
			var dbreq = indexedDB.deleteDatabase(idbfs.dbname);
			dbreq.onsuccess = function() {
				if (oncomplete) oncomplete();
			};
			dbreq.onerror = function() {
				console.error("IDBFS cleanup error.");
				if (onerror) onerror();
			};
			idbfs = undefined;
		};

		CriFs.IDB.writeFile = function(path, data, oncomplete, onerror) {
			if (path.indexOf("idbfs:") == 0) path = path.slice(6);

			// ファイルの実体をIDBに書き込み
			function writeEntities(data, oncomplete, onerror) {
				var tr = idbfs.db.transaction("entities", "readwrite");
				var store = tr.objectStore("entities");
				var entities = [];

				// 分割サイズ
				var entitySize = 256 * 1024;
				
				// ファイル実体(1ブロック分)をIDBに書き込み
				function writeEntity(data, offset, length) {
					var entityData = new Uint8Array(data, offset, length);
					var req = store.put(entityData);
					req.onsuccess = function(e) {
						entities.push({key: e.target.result, offset: offset, length: length});
					}
					req.onerror = onerror;
				}

				for (var offset = 0; offset < data.byteLength; ) {
					var length = (offset + entitySize < data.byteLength) ? entitySize : data.byteLength - offset;
					writeEntity(data, offset, length);
					offset += length;
				}
				tr.oncomplete = function(){
					oncomplete(entities, data);
				}
				tr.onerror = onerror;
			}

			// ファイルのTOC情報を書き込み
			function writeToc(path, size, entities, oncomplete, onerror) {
				var tr = idbfs.db.transaction("toc", "readwrite");
				var store = tr.objectStore("toc");
				var tocData = {
					path: path,
					size: size,
					entities: entities
				};
				var req = store.add(tocData);
				//req.onsuccess = function(e) {}
				req.onerror = onerror;
				tr.oncomplete = function(){
					oncomplete(tocData);
				};
				tr.onerror = onerror;
			}

			// ファイル実体書込完了
			function onCompletedWriteEntities(entities, data) {
				this.tr = writeToc(path, data.byteLength, entities, 
					onCompletedWriteToc, onerror);
			}
			// ファイルTOC書込完了
			function onCompletedWriteToc(tocData) {
				idbfs.files[path] = tocData;
				if (oncomplete) oncomplete();
			}

			writeEntities(data, onCompletedWriteEntities, onerror);
		}
		CriFs.IDB.readFile = function(path, oncomplete, onerror) {
			if (path.indexOf("idbfs:") == 0) path = path.slice(6);
		};
		CriFs.IDB.existsFile = function(path) {
			if (path.indexOf("idbfs:") == 0) path = path.slice(6);
			return (path in idbfs.files);
		};
		CriFs.IDB.removeFile = function(path, oncomplete, onerror) {
			if (path.indexOf("idbfs:") == 0) path = path.slice(6);
			var file = idbfs.files[path];
			if (file) {
				delete idbfs.files[path];
				var tr = idbfs.db.transaction(["toc", "entities"], "readwrite");
				var tocStore = tr.objectStore("toc");
				var entitiesStore = tr.objectStore("entities");
				tocStore.delete(path);
				for (var i in file.entities) {
					entitiesStore.delete(file.entities[i].key);
				}
				tr.oncomplete = oncomplete;
				tr.onerror = onerror;
			} else {
				if (onerror) onerror();
			}
		};
		CriFs.IDB.renameFile = function(oldpath, newpath, oncomplete, onerror) {
			if (oldpath.indexOf("idbfs:") == 0) oldpath = oldpath.slice(6);
			if (newpath.indexOf("idbfs:") == 0) newpath = newpath.slice(6);
			var file = idbfs.files[oldpath];
			if (file) {
				delete idbfs.files[oldpath];
				file.path = newpath;
				idbfs.files[newpath] = file;
				var tr = idbfs.db.transaction("toc", "readwrite");
				var tocStore = tr.objectStore("toc");
				tocStore.delete(oldpath);
				tocStore.put(file);
				tr.oncomplete = oncomplete;
				tr.onerror = onerror;
			} else {
				if (onerror) onerror();
			}
		};

		var memfs;
		if (typeof FS !== "undefined") {
			// For Unity
			CriFs.DEF = CriFs.MEM;
			// Emscripten FileSystem
			memfs = FS;
		} else {
			// For Web
			CriFs.DEF = CriFs.XHR;
			// Tiny Memory FileSystem
			memfs = {
				files: {},
				stat: function(path) {
					var file = memfs.files[path];
					if (file) {
						return {size: file.buf.byteLength};
					}
					throw {};
				},
				open: function(path, opt) {
					if (opt[0] == 'w') {
						return memfs.files[path] = {buf:null};
					} else  if (path in memfs.files) {
						return memfs.files[path];
					}
					throw {};
				},
				close: function() {},
				read: function(stm, u8buf, ptr, size, offset) {
					size = Math.min(stm.buf.length - offset, size);
					u8buf.subarray(ptr, ptr + size).set(stm.buf.subarray(offset, offset + size));
					return size;
				},
				write: function(stm, u8buf, ptr, size, offset) {
					stm.buf = new Uint8Array(size);
					stm.buf.set(u8buf, ptr);
				},
				syncfs: function(flag, oncomplete) {
					if (oncomplete) oncomplete();
				},
				unlink: function(path) {
					var file = memfs.files[path];
					if (file) delete memfs.files[path];
					else throw {};
				},
				rename: function(oldpath, newpath) {
					var file = memfs.files[oldpath];
					if (file && !(newpath in memfs.files)) {
						delete memfs.files[oldpath];
						memfs.files[newpath] = file;
					} else {
						throw {};
					}
				}
			};
		}

		// ファイルシステムの選択
		CriFs.selectfs = function(path, localfs) {
			var fs = null;
			if (path.indexOf("http:") == 0 || path.indexOf("https:") == 0 || path.indexOf("blob:http:") == 0 || path.indexOf("blob:https:") == 0) {
				fs = CriFs.XHR;
			} else if (memfs && path.indexOf("memfs:") == 0) {
				fs = CriFs.MEM;
			} else if (idbfs && path.indexOf("idbfs:") == 0) {
				fs = CriFs.IDB;
			} else if (path.indexOf(":") < 0) {
				fs = CriFs.DEF;
			}
			if (localfs && fs === CriFs.XHR) {
				console.error("Invalid local path: " + path);
				fs = null;
			}
			return fs;
		};
		
		CriFs.writeFile = function(path, data, oncomplete, onerror) {
			var fs = CriFs.selectfs(path, true);
			if (fs) fs.writeFile(path, data, oncomplete, onerror);
			else if (onerror) onerror();
		};
		CriFs.existsFile = function(path) {
			var fs = CriFs.selectfs(path, true);
			return fs ? fs.existsFile(path) : false;
		};
		CriFs.removeFile = function(path, oncomplete, onerror) {
			var fs = CriFs.selectfs(path, true);
			if (fs) fs.removeFile(path, oncomplete, onerror);
			else if (onerror) onerror();
		};
		CriFs.renameFile = function(oldpath, newpath, oncomplete, onerror) {
			var oldfs = CriFs.selectfs(oldpath, true);
			var newfs = CriFs.selectfs(newpath, true);
			if (!oldfs || !newfs) {
				if (onerror) onerror();
				return;
			}
			if (oldfs === newfs) {
				// 同じFSなのでrenameFileが使える
				oldfs.renameFile(oldpath, newpath);
			} else {
				// 違うFSなので手間がかかる
				oldfs.readFile(oldpath, function(data){
					oldfs.removeFile(oldpath, function() {
						newfs.writeFile(newpath, data, oncomplete, onerror);
					}, onerror);
				}, onerror);
			}
		};

		function makeCrcTable() {
			var table = new Uint32Array(256);
			for (var i = 0; i < 256; i++) {
				var x = i;
				for (var j = 0; j < 8; j++) {
					x = (x & 1) ? (0xEDB88320 ^ (x >>> 1)) : (x >>> 1);
				}
				table[i] = x;
			}
			return table;
		}
		function calcCrc(data) {
			var crc = 0xFFFFFFFF;
			var table = CriFs.insMgr.crcTable;
			if (table) {
				// 有効時のみCRC計算を行う
				var buf = new Uint8Array(data);
				var length = buf.length;
				for (var i = 0; i < length; i++) {
					crc = table[(crc ^ buf[i]) & 0xFF] ^ (crc >>> 8);
				}
			}
			return crc ^ 0xFFFFFFFF;
		}
		CriFs.Installer = function() {
			this.status = 0;
			this.errorCode = 0;
			this.httpCode = -1;
			this.contentsSize = -1;
			this.recievedSize = 0;
			this.crc = 0;
		}
		CriFs.Installer.prototype = {
			start: function(url, path) {
				this.path = path;
				this.status = 1;
				this.xhr = new CriFs.XHR();
				this.xhr.onprogress = this._onXHRProgress.bind(this);
				this.xhr.ontimeout = this._onXHRTimeout.bind(this);
				this.xhr.start(url, 0, 0xffffffff, 
					this._onXHRCompleted.bind(this), this._onXHRError.bind(this), 
					CriFs.insMgr.headers, CriFs.insMgr.timeout);
			},
			stop: function() {
				if (this.xhr) this.xhr.stop();
				this.status = 0;
				this.errorCode = 0;
				this.httpCode = -1;
				this.contentsSize = -1;
				this.recievedSize = 0;
			},
			_onXHRProgress: function(e) {
				this.httpCode = this.xhr.status;
				this.contentsSize = e.total;
				this.recievedSize = e.loaded;
			},
			_onXHRTimeout: function() {
				this.status = 3;
				this.errorCode = 1;
			},
			// HTTP転送完了
			_onXHRCompleted: function(contentLength) {
				var data = this.xhr.req.response;
				this.crc = calcCrc(data);
				this.httpCode = this.xhr.req.status;
				this.contentsSize = contentLength;
				this.xhr = null;
				
				CriFs.writeFile(this.path, data, 
					this._onWriteCompleted.bind(this), 
					this._onWriteError.bind(this));
			},
			// HTTP転送エラー
			_onXHRError: function(e) {
				this.status = 3;
				var httpCode = this.xhr.req.status;
				if (httpCode) {
					this.httpCode = httpCode;
					this.errorCode = 7;
				} else {
					this.errorCode = 5;
				}
				this.recievedSize = 0
				this.contentsSize = -1;
				this.xhr = null;
			},
			// ファイル書き込み完了
			_onWriteCompleted: function() {
				this.status = 2;
			},
			// ファイル書き込みエラー
			_onWriteError: function(e) {
				this.status = 3;
				this.errorCode = 3;
			}
		};
		CriFs.Installer.setConfig = function(maxIns, timeout, crc) {
			CriFs.insMgr.maxIns = maxIns;
			CriFs.insMgr.timeout = 1000 * timeout;
			CriFs.insMgr.crcTable = (crc) ? makeCrcTable() : null;
		}
	},
},
criFsWeb_Init: function() {
	if (!CriFs.initialized) {
		CriFs.init();
		CriFs.initialized = true;
		Module["CriFsItf"] = {
			"setupIDBFS": CriFs.IDB.init,
			"clearIDBFS": CriFs.IDB.clear,
			"writeFile": CriFs.writeFile,
			"existsFile": CriFs.existsFile,
			"removeFile": CriFs.removeFile,
			"renameFile": CriFs.renameFile,
		};
	}
},
criFsWeb_SetupIDBFS: function(dbname) {
	CriFs.IDB.init(UTF8ToString(dbname));
},
criFsWeb_ClearIDBFS: function() {
	CriFs.IDB.clear();
},
criFsWeb_SetRequestHeader: function(field, value) {
	var headers = CriFs.xhr.headers;
	var fieldStr = UTF8ToString(field);
	if (value === 0) {
		delete headers[fieldStr];
	} else {
		headers[fieldStr] = UTF8ToString(value);
	}
},
criFsWeb_SetRequestTimeout: function(timeout) {
	CriFs.xhr.timeout = timeout;
},
criFsWeb_Start: function(handle, urlptr, offset, size, onloadCb, onerrorCb) {
	var url = UTF8ToString(urlptr);
	var onload = function(contentLength) {
  		dynCall("vii", onloadCb, [handle, contentLength]);
	}
	var onerror = function() {
  		dynCall("vi", onloadCb, [handle]);
	}
	//console.log("criFsWeb_Start: ", url, offset, size);
	var fs = CriFs.selectfs(url);
	if (fs === null) {
		onerror();
		return;
	}
	var req = CriFs.reqs[handle] = new fs();
	req.start(url, offset, size, onload, onerror);
},
criFsWeb_Stop: function(handle) {
	//console.log("criFsWeb_Stop", buffer, offset, size);
	var req = CriFs.reqs[handle];
	if (req) {
		req.stop();
		delete CriFs.reqs[handle];
	}	
},
criFsWeb_Read: function(handle, buffer, offset, size) {
	//console.log("criFsWeb_Read", buffer, offset, size);
	var req = CriFs.reqs[handle];
	if (req) {
		return req.read(buffer, offset, size);
	}
	return 0;
},
criFsWeb_SetInstallerConfig: function(maxIns, timeout, crc) {
	CriFs.Installer.setConfig(maxIns, timeout, crc);
},
criFsWeb_CreateInstaller: function() {
	if (CriFs.insMgr.curIns >= CriFs.insMgr.maxIns) {
		return 0;
	}
	var handle = ++CriFs.insMgr.unique;
	var installer = new CriFs.Installer();
	CriFs.ins[handle] = installer;
	CriFs.insMgr.curIns++;
	return handle;
},
criFsWeb_DestroyInstaller: function(handle) {
	var installer = CriFs.ins[handle];
	if (installer) {
		delete CriFs.ins[handle];
		CriFs.insMgr.curIns--;
	}
},
criFsWeb_StartInstall: function(handle, urlptr, pathptr) {
	var installer = CriFs.ins[handle];
	var url = UTF8ToString(urlptr);
	var path = UTF8ToString(pathptr);
	installer.start(url, path, null, null);
},
criFsWeb_StopInstall: function(handle) {
	var installer = CriFs.ins[handle];
	installer.stop();
},
criFsWeb_GetInstallerStatusInfo: function(handle, info) {
	var installer = CriFs.ins[handle];
	HEAPU32[(info+0)>>2] = installer.status;
	HEAPU32[(info+4)>>2] = installer.errorCode;
	HEAPU32[(info+8)>>2] = installer.httpCode;
	HEAPU32[(info+16)>>2] = installer.contentsSize;
	HEAPU32[(info+24)>>2] = installer.recievedSize;
},
criFsWeb_GetInstallerCRC32: function(handle) {
	var installer = CriFs.ins[handle];
	return installer.crc;
},
criFsWeb_SetInstallerRequestHeader: function(field, value) {
	CriFs.insMgr.headers[UTF8ToString(field)] = UTF8ToString(value);
}
};

autoAddDeps(LibraryCriFs, '$CriFs');
mergeInto(LibraryManager.library, LibraryCriFs);
