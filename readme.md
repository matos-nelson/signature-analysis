# Signature Analysis Application

## Requirements
Write a command line application (in C#) that does the following: 
1. Takes two inputs and a flag
    a. A directory that contains the files to be analyzed
    b. A path for the output file (including file name and extension)
    c. A flag to determine whether or not to include subdirectories contained (and all subsequently embedded subdirectories) within the input directory ([a.] above)
2. Processes each of the files in the directory (and subdirectories if the flag is present)
3. Determines using a file signature if a given file is a PDF or a JPG
    a.  JPG files start with 0xFFD8
    b. PDF files start with 0x25504446
4. For each file that is a PDF or a JPG, creates an entry in the output CSV containing the following information
    a. The full path to the file
    b. The actual file type (PDF or JPG)
    c. The MD5 hash of the file contents

## Project Layout
The project uses .Net Core. This will allow the program to be compatiable with multiple environments.
The project utilizes the clean architecture approach. There are 3 separate projects: Core, Infrastructure, ConsoleApp. Using the clean architecture approach will allow separation of concerns by dividing the software into layers. With this in mind, applications can be designed with very low connection and independent of implementation details. This will allow the application to become easy to maintain, flexible to change, and easy to implement unit/integration testing methods.

Read More Here - [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)

## Design Considerations
1. The requirements only listed to support two file signatures. The approach used will make supporting more file signatures trivial. 
There will need to be two changes done to the application.
    1. Provide a Enum value to FileSignature enum.
    2. Create a class that inherits BaseFile, and create a constructor that initializes the correct type and signatures.
    
    The infrastructure layer will search all the assemblies for types that inherit BaseFile and include that object type into the Signature Analysis process. The BaseFile abstract class is very flexible. The two file signatures provided in the requirements are generic to its type. The BaseFile can contain a list of signatures. This will allow the BaseFile to keep track of multilple very specific signatures and will sill be able to determine the correct File Signature.

2. Implemented a Boss Worker thread pattern into the Signature Analysis Process. This will help scale the application that needs to process a large amount of files. Currently the number of workers is hardcoded. The boss worker thread will gather the entire file listing without any filters and will place the files into a thread safe queue. Once the queue has been established, the worker threads will retrieve an item from the queue, process that item only if its a supported file type and store the results into another thread safe queue. Once all the items have been process, the results will be logged to the location provided. The logging is done by a single thread. 

## Testing Methodoloy
Most of the testing was done by full integration, meaning I will actually run the compiled program and run it with various arguments. Af the program has finished executing, I will verify the md5 hash against an online tool.
Online tools used to compare implementation:
    1. https://emn178.github.io/online-tools/md5_checksum.html
    2. http://onlinemd5.com/
- Tested various argument combinations, such as incorrect process folder, correct file path, incorrect value for flag.
- Tested files located in only the top level folder provided with the flag set to false.
- Tested files located in the top level folder provided and subfolders with the flag set to true.
- Tested with valid JPG,PDF files with correct signature and correct extension. IE. image.jpg and this file was a valid jpg file.
- Tested with invalid JPG,PDF files with incorrect signature and correct extension. IE. image.jpg and this file was a empty text file.
- Tested with valid JPG,PDF files with correct signature and incorrect extension. IE. image.txt and this file was a valid jpg.
- Tested with combinations small and large files. File size ranged from 1kb to 30mb.

## How To Run
Special Note: Given paths with spaces are not supported.

```
SignatureAnalysis.ConsoleApp.exe "Absolute Path To Process" "Absolute Path Including Filename and Extenstion" true/false
```

Example:
```
SignatureAnalysis.ConsoleApp.exe "C:/Files" "C:/output.csv" false
```


