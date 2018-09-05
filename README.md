# ao-id-extractor
This tool allows you to extract the ID's and Names of Items and Locations from the Albion Online ".bin" Files, and export them to a file as a List or JSON.

## Requirements
* Windows
* Visual Studio 2017 (to build)

## How to use:
`ao-id-extractor.exe modeID outFormat [outFolder] [gameFolder]`

modeID          `0=Item Extraction, 1= Location Extraction, 2=Resource, 3=Dump All, 4=Extract Items & Locations & Resource`  
outFormat       `l=Text List, j=JSON; Parameter does not matter for Dump All`  
[outFolder]     `Optional: Output folder path. Default: current directory`
[gameFolder]    `Optional: Location of the main AlbionOnline folder`
