# Text Playground
## Description:
This solution contains two projects:<br/>
 - **TextGenerator** - create sample random text data in size given by the user (1-100GB).
 - **TextSorter** - sort generated previously data by text and then by number.

 To download ready to use applications you can use build pipeline in the repository, here: [Build pipeline](https://github.com/qwerty-bug/text-sorter/actions/workflows/dotnet.yml)
 You can also build solution using your favourite IDE.
</br> As artifact you will get two packages to download:
 - TextGenerator
 - TextSorter

## How to use:
**TextGenerator** - will produce file `SAMPLE_DATA.txt` in application folder with all sample data.
</br>**TextSorter** - expect to find file `SAMPLE_DATA.txt` in application folder. The sorted output file will be saved in file `SORTED_DATA_<HH>_<MM>_<SS>.txt` in application folder with timestamp.

---
 ##### Author: Piotr Zawisza