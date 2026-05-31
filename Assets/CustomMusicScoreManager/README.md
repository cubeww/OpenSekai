# Custom Music Score Manager

This folder contains the local-only custom music score manager.

- `Runtime/Models`: manifest and custom score entry data types.
- `Runtime/Services`: filesystem scanning, import/export, and entry loading.
- `Runtime/UI`: UGUI screens and widgets.
- `Resources/Prefabs`: prefabs loaded through Unity `Resources`.
- `Resources/Screen/Prefabs`: screen-layer prefabs loaded by Sekai's `ScreenLayerData`.
- `Sprites` and `Materials`: UI assets owned by the manager.
- `Editor`: editor-only helpers.

Keep custom score manager code and assets here so imported Project Sekai assets stay separate.

The first local manager screen is implemented as `ScreenLayerMusicScoreMakerTop.prefab`.
It intentionally reuses the existing `MenuScreenType.MusicScoreMakerTop` entry so we do not
need to add another global screen enum or modify `EntryScreenLayers.asset`.

Local score entries are stored under:

`Application.persistentDataPath/CustomMusicScores/<title>_<shortId>/`

Normal non-auto play results are appended to:

`Application.persistentDataPath/CustomMusicScores/PlayHistory.jsonl`

Expected entry files:

- `manifest.json`
- `score.json`
- audio file named by `manifest.audioFileName`
- jacket image named by `manifest.jacketFileName`

The manager's Score selector accepts `score.json` directly or an official SUS `.txt` / `.sus`;
SUS files are converted to the local `score.json` format on import.

`manifest.title` is the song title shown in live MusicInfo. `manifest.scoreTitle`
is the custom score title shown in the score-info area.
`manifest.collaborationLabel` controls the MusicInfo collaboration ribbon; leave it
empty to hide the ribbon.
