# OPML
OPML is OPML Parser

It supports .NET Core 2.0.

# Usage
 How to use

```
using OPML.Core;

Opml opml = new Opml("filePath");

foreach (Outline outline in opml.Body.Outlines) 
{
    Console.WriteLine(outline.Text);
    Console.WriteLine(outline.XmlUrl);
    
    foreach (Outline childOutline in outline.Outlines)
    {
        Console.WriteLine(childOutline.Text);
        Console.WriteLine(childOutline.XmlUrl);                    
    }
}
```

For more detail, show test code or source code.

# License
Lisense is MIT License.
