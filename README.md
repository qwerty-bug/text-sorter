# Text Playground
## Description:
This solution contains two projects:<br/>
 - **TextGenerator** - create sample random text data in size given by the user (1-100GB).
 - **TextSorter** - sort generated previously data by text and then by number.

 To download ready to use applications please use build pipeline in the repository, here: [Build pipeline](https://github.com/qwerty-bug/text-sorter/actions/workflows/dotnet.yml)
 You can also build solution using your favourite IDE.
</br> As artifact you will get two packages to download:
 - TextGenerator
 - TextSorter

 > Please run **TextSorter** with bat file `StartSorter.bat` (in app folder). It will run `TextSorter.exe` and set high priority to this task on Windows system.

 Logs for both application are stored in `log` folder of each app home directory.

## How to use:
**TextGenerator** - Start `TextGenerator.exe`, it will produce file `SAMPLE_DATA.txt` in application folder with all sample data in size given by user input.
</br> **TextSorter** - Run `StartSorter.bat` bat file. TextSorter is expecting to find file `SAMPLE_DATA.txt` in application folder. The sorted output file will be saved in file `SORTED_DATA_<HH>_<MM>_<SS>.txt` with timestamp in file name.

---
 ##### Author: Piotr Zawisza