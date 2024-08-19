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

## Samlpe run
For 70GB of text:
```
2024-08-18 17:30:15,678 [1] INFO, [3403.823s] Data sorted successfully.
2024-08-18 17:30:15,811 [1] INFO, [3403.955s] ============================================
2024-08-18 17:30:15,811 [1] INFO, [3403.955s] 
2024-08-18 17:30:15,811 [1] INFO, [3403.955s] ><><><><>><><><><><><>><><><><><><>><><><><><><><
2024-08-18 17:30:15,811 [1] INFO, [3403.955s] 
2024-08-18 17:30:15,811 [1] INFO, [3403.955s] STATS:
2024-08-18 17:30:20,589 [1] INFO, [3403.955s]  Total processing time: 3403.95s [00:56:43.95]
2024-08-18 17:30:37,313 [1] INFO, [3403.955s]  Average:               48.63s per 1GB
2024-08-18 17:30:37,313 [1] INFO, [3403.955s]  Output file:           'SORTED_DATA_16_48_24.txt'
2024-08-18 17:30:37,331 [1] INFO, [3403.955s]  Output file size:      70GB
2024-08-18 17:30:37,331 [1] INFO, [3403.955s] 
2024-08-18 17:30:37,331 [1] INFO, [3403.955s] ><><><><>><><><><><><>><><><><><><>><><><><><><><
2024-08-18 17:30:37,331 [1] INFO, [3403.955s] 
```

---
Used materials:
- Merge Algorithms - 2-Way and K-Way Merge: https://www.youtube.com/watch?v=Xo54nlPHSpg
- Sorting (really) large files with C#: https://josef.codes/sorting-really-large-files-with-c-sharp/

---
 ##### Author: Piotr Zawisza
