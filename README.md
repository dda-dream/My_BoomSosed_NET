# My_BoomSosed_NET

My_BoomSosed_NET is a small Windows desktop utility (WinForms) built on .NET 9 that visualizes a configurable grid and plays sounds from local playlists on a scheduler. It is intended for simple timed playback scenarios with an optional randomization of time and volume.

## Key features
- Visual "boom" grid that highlights cells and triggers sounds.
- Selectable sound folders (playlists) and files; option to play random files.
- Scheduler with time window, random intervals and random volume.
- Simple local configuration saved to `config.cfg`.
- Uses NAudio for audio playback.

## Requirements
- .NET 9 runtime (or SDK to build)
- Windows x64
- NAudio package (already referenced in project)

## Quick build & publish
Build and run from Visual Studio 2022, or use the CLI:
- Build/publish single-file self-contained release:
  __dotnet publish__ My_BoomSosed_NET.csproj -o bin\publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -p:EnableCompressionInSingleFile=true

## Usage
1. Place playlists under the ./sounds/ directory (each folder is a playlist).
2. Start the app, select a playlist and (optionally) a file.
3. Configure speed, fill ratio and scheduler options, then click Start.
4. The grid will iterate and play sounds for cells marked by the fill ratio.

## Configuration & logs
- Config is stored in `config.cfg` (plain text key = value format).
- Application log is written to `log.txt` and shown in the app log panel.

## Publishing & git
- To upload changes:
  __git add .__
  __git commit -m "your message"__
  __git push origin main__

## Notes / TODO
- No license specified — add one if you plan to publish.
- Improve error handling and async audio queueing in future iterations.


