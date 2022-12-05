This program consumes the Web API hosted on http://gendacproficiencytest.azurewebsites.net/API/ProductsAPI/ by making use of the .NET Desktop Runtime Environment.

An installer for the required .NET framework is included in the "redist" folder.

To run the program, execute APIConsumer.exe

To enable logging in "Program Files" or other locations requiring Administrative Write Permissions, run the program as administrator. 
The created log file is Log.txt 

Upon startup, the program will retrieve a list of all products from the API. The products are organised in rows with each column showing the specific product's Id, Name, Category and Price. 
An updated list may be obtained at any time during execution, by clicking the "Get Product List" button.

Products with specific IDs may be obtained by clicking on the "Get Id" button, which will open a dialog asking for the Id to retrieve. Press "OK" to retrieve the product with the specified Id from the API.

Entries in the product list may be deleted or edited by first selecting the desired product and pressing either the "Delete" or "Edit..." buttons. When editing, enter the desired product details in the presented dialog and press the "OK" button.

Products may be added to the list by clicking on the "Add product" button, entering the desired product details in the presented dialog and pressing the "OK" button.

Finally, if the criteria for the products obtained from the API must be refined, the "Get ordered, filtered, sorted list" button should be pressed. The user is then prompted to enter a page, page size, 
attribute to order by as well as if the result should be in ascending/descending order, and a filter - which will be applied to any product attribute which even partially matches the filter. Press the "OK" button once
the desired criteria have been specified.
