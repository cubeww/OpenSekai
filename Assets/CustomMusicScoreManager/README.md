# Custom Music Score Manager

This folder contains the local-only custom music score manager.

- `Runtime/Models`: manifest and score package data types.
- `Runtime/Services`: filesystem scanning, import/export, and package loading.
- `Runtime/UI`: UGUI screens and widgets.
- `Resources/Prefabs`: prefabs loaded through Unity `Resources`.
- `Resources/Screen/Prefabs`: screen-layer prefabs loaded by Sekai's `ScreenLayerData`.
- `Sprites` and `Materials`: UI assets owned by the manager.
- `Editor`: editor-only helpers.

Keep custom score manager code and assets here so imported Project Sekai assets stay separate.

The first local manager screen is implemented as `ScreenLayerMusicScoreMakerTop.prefab`.
It intentionally reuses the existing `MenuScreenType.MusicScoreMakerTop` entry so we do not
need to add another global screen enum or modify `EntryScreenLayers.asset`.

Local packages are stored under:

`Application.persistentDataPath/CustomMusicScores/<title>_<shortId>/`

Expected package files:

- `manifest.json`
- `score.json`
- audio file named by `manifest.audioFileName`
- jacket image named by `manifest.jacketFileName`

The manager's Score selector accepts `score.json` directly or an official SUS `.txt`;
SUS files are converted to the local `score.json` format on import.

`manifest.title` is the song title shown in live MusicInfo. `manifest.scoreTitle`
is the custom score title shown in the score-info area.
