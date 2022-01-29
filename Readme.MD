How to use:
In your program, put "using FileHelper;"
This package adds 2 new classes: folder and file
To create a new folder object, just use "new folder()"
When creating a new file object, there are 2 constructors: Path and Fctype
Fctype tells the new object what to do: create a new file (fctype.create), or set the object to an already existing file (fctype.normal).
Path is the path of the file.
This has most System.IO.File methods built into the file class, with only some less used ones left out.
To delete a file, just use the .delete() method of the file object.
IMPORTANT: After deleting a file using a file object, stop using the file object OR use [fileobjectname].reinitalize() and use the same constructors as you would with "new file()".

You can also move a file object to a file on your computer of a different type by using "FileHelper.fms.convert()" with the 2 arguments being the file object and the other file's path.
