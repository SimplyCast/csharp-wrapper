<h1>SimplyCast C# Wrapper</h1>
	
<h2>Description</h2>
	
<p>The SimplyCast C# Wrapper helps facilitate integration with the SimplyCast
API. It performs authentication and serialization for many of the resources
in the API and includes some additional helper functionality beyond what the
API provides.<p>
	
<h2>Usage</h2> 
	
<p>To use this software, open the project in your IDE and build it (in Visual
studio, hit F6). Then include the resulting DLL in your project as a 
reference. Instructions on adding a reference to your project can be found 
on the Microsoft MSDN site by searching for	"Add and Remove References in 
Visual Studio (C#)".</p>
	
<p>Examples of how to use the API wrapper are included in the file Examples.cs;
there are a series of methods in the SimplyCast.Examples class that run 
through different categories of functionality.</p>
	
<p>Here is an example of initializing the API wrapper and calling examples or
API methods:</p>
	
```csharp
string publicKey = "<public>";
string secretKey = "<secret>";

SimplyCastAPI api = new SimplyCastAPI(publicKey, secretKey);

SimplyCast.Examples examples = new SimplyCast.Examples(api);
e.ContactManagementExample();

SimplyCast.ContactManager.Responses.ContactCollection contacts = 
  api.ContactManager.GetContacts();
```
	
<h2>Licensing</h2>
	
<p>This software is distributed under the MIT licence. Please see the attached 
file called LICENCE.txt.</p>