# OpenSekai

OpenSekai 是一个用于学习和研究目的的 Project Sekai 音乐游戏玩法复刻项目，重点还原 live 场景中的谱面解析、音符渲染、判定表现、UI 动画、音效播放以及结果展示等核心流程。

项目目前仍处于早期开发阶段，代码结构会尽量贴近原作中的命名和运行时数据模型，方便后续逐步对齐行为细节。

## 项目目标

- 还原 live 玩法的核心运行流程。
- 支持 SUS 谱面解析并转换为运行时 `MusicScore` 数据。
- 复刻音符、长条、滑键、判定特效、分数、血条、Auto 模式等 live 表现。
- 尽量保持对象层级、类名和数据结构与原作逻辑接近。

## 当前内容

- `Assets/Scripts/Sekai`：live 玩法相关脚本与运行时逻辑。
- `Assets/Features/live`：live 场景使用的 prefab、材质、纹理、音效等资源。
- `Assets/Charts`：谱面和音乐测试资源。
- `Assets/Shaders`：为还原原作表现补写或适配的 shader。
- `Assets/Scenes`：Unity 场景文件。

## 开发环境

- Unity 6.3 LTS 或兼容版本。

部分对象引用需要在 Inspector 中手动拖拽配置，项目代码不会在运行时动态加载 Unity 资源。

## 资源说明

本仓库中的代码以 MIT 协议发布。第三方库、字体、音频、贴图、prefab、shader 参考资源以及任何可能来源于原作或其他权利方的资源，不自动包含在 MIT 授权范围内，请分别遵循其原始许可和权利归属。

本项目与任何原作游戏、发行方或权利方没有官方关联。

## License

Code is licensed under the MIT License. See [LICENSE](LICENSE) for details.
