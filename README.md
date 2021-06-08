# Jsonize
Convert HTML to JSON with this .NET package.

## Version 1.0.9
This version has been archived and is available to download from the release page.

## Version 2.0.0
This version was abandoned. See branch `jsonize-2.0.0` if you are interested.

## Version 3.\*.\*
Version 3.0.0 introduces breaking changes to the Jsonize project. 
The project has been completely rewritten to decouple, simplify, and keep up with new standards.
Version `3.*.*` will drop the `JackWFinlay` portion of the namespace, e.g. `JackWFinlay.Jsonize` is now `Jsonize`.

The project now splits the parsing and serialization into separate areas of concern, 
as noted by the introduction of the `IJsonizeParser` and `IJsonizeSerializer` interfaces,
found in the `Jsonize.Abstractions` package.
These can be implemented by anyone, 
but a brand new parser has been written using AngleSharp as its HTML engine.
This is supplied as the `Jsonize.Parser` package.
There is available a serialization helper that conforms to the same standard as previous versions,
using the `System.Text.Json` library.
There is also the `Jsonize.Serializer.Json.Net` package,
which implements a basic serializer wrapping the `Newtonsoft.Json` package with some useful functions.
Feel free to implement your own serializer.

The `Jsonize` package simply wraps the parser and serializer functions into one.
Jsonize no longer will grab any content from the internet for you;
you must supply the HTML as a `string` to `Jsonizer` class methods.

## Try it

Get the NuGet packages: 

**Package**|**Build Status**|**NuGet Version**
:-----|:-----:|:-----:
[Jsonize](https://www.nuget.org/packages/Jsonize/)|![.NET Core](https://github.com/JackWFinlay/jsonize/workflows/.NET%20Core/badge.svg)|![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Jsonize)
[Jsonize.Abstractions](https://www.nuget.org/packages/Jsonize.Abstractions/)|![.NET Core](https://github.com/JackWFinlay/jsonize/workflows/.NET%20Core/badge.svg)|![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Jsonize.Abstractions)
[Jsonize.Parser](https://www.nuget.org/packages/Jsonize.Parser/)|![.NET Core](https://github.com/JackWFinlay/jsonize/workflows/.NET%20Core/badge.svg)|![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Jsonize.Parser.AngleSharp)
[Jsonize.Serializer.Json.Net](https://www.nuget.org/packages/Jsonize.Serializer.Json.Net/)|![.NET Core](https://github.com/JackWFinlay/jsonize/workflows/.NET%20Core/badge.svg)|![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Jsonize.Serializer.Json.Net)


## Usage

An example to get the site "https://jackfinlay.com" as a JSON string:

```C#
private static async Task<string> Testy(string q = "")
{
    using (var client = new HttpClient())
    {
        string url = @"https://jackfinlay.com";

        HttpResponseMessage response = await client.GetAsync(url);

        string html = await response.Content.ReadAsStringAsync();

        // The use of the parameterless constructors will use default settings.
        JsonizeParser parser = new JsonizeParser();
        JsonizeSerializer serializer = new JsonizeSerializer();        

        Jsonizer jsonizer = new Jsonizer(parser, serializer);

        return jsonizer.ParseToStringAsync();
    }
}
```

Alternatively, get the response as a `JsonizeNode`:

```C#
return jsonizer.ParseToJsonizeNodeAsync();
```

You can control the output with a `JsonizeParserConfiguration` object, which is passed as a parameter to the constructor of the `IJsonizeParser` of choice:

```C#
JsonizeParserConfiguration parserConfiguration = new JsonizeParserConfiguration()
{
    NullValueHandling = NullValueHandling.Ignore,
    EmptyTextNodeHandling = EmptyTextNodeHandling.Ignore,
    TextTrimHandling = TextTrimHandling.Trim,
    ClassAttributeHandling = ClassAttributeHandling.Array
}

JsonizeConfiguration jsonizeConfiguration = new JsonizeConfiguration
{
    Parser = new JsonizeParser(parserConfiguration),
    Serializer = new JsonizeSerializer()
};

Jsonizer jsonizer = new Jsonizer(jsonizeConfiguration);
```

Results are in the form:
```JSON
{
    "nodeType":"Node type e.g. Document, Element, or Comment",
    "tag":"If node is Element this will display the tag e.g p, h1 ,div etc.",
    "text":"If node is of type Text, this will display the text in that node.",
    "attr":{
                "name":"value",
                "class": []
            },
    "children":[
                {
                    "nodeType":"Node type e.g. Document, Element, or Comment",
                    "tag":"If node is Element this will display the tag e.g p, h1 ,div etc.",
                    "text":"If node is of type Text, this will display the text in that node.",
                    "child": []
                }
            ]
}
```

## Example:

The following HTML:
```HTML
<!DOCTYPE html>
<html>
    <head>
        <title>Jsonize</title>
    </head>
    <body>
        <div id="parent" class="parent-div">
            <div id="child1" class="child-div child1">Some Text</div>
            <div id="child2" class="child-div child2">Some Text</div>
            <div id="child3" class="child-div child3">Some Text</div>
        </div>
    </body>
</html>
```

Becomes:
```JSON
{
    "nodeType": "Document",
    "tag": null,
    "text": null,
    "attr": {},
    "children": [
        {
            "nodeType": "DocumentType",
            "tag": "html",
            "text": null,
            "attr": {},
            "children": []
        },
        {
            "nodeType": "Element",
            "tag": "html",
            "text": null,
            "attr": {},
            "children": [
                {
                    "nodeType": "Element",
                    "tag": "head",
                    "text": null,
                    "attr": {},
                    "children": [
                        {
                            "nodeType": "Element",
                            "tag": "title",
                            "text": "Jsonize",
                            "attr": {},
                            "children": []
                        }
                    ]
                },
                {
                    "nodeType": "Element",
                    "tag": "body",
                    "text": null,
                    "attr": {},
                    "children": [
                        {
                            "nodeType": "Element",
                            "tag": "div",
                            "text": null,
                            "attr": {
                                "id": "parent",
                                "class": [
                                    "parent-div"
                                ]
                            },
                            "children": [
                                {
                                    "nodeType": "Element",
                                    "tag": "div",
                                    "text": "Some Text",
                                    "attr": {
                                        "id": "child1",
                                        "class": [
                                            "child-div",
                                            "child1"
                                        ]
                                    },
                                    "children": []
                                },
                                {
                                    "nodeType": "Element",
                                    "tag": "div",
                                    "text": "Some Text",
                                    "attr": {
                                        "id": "child2",
                                        "class": [
                                            "child-div",
                                            "child2"
                                        ]
                                    },
                                    "children": []
                                },
                                {
                                    "nodeType": "Element",
                                    "tag": "div",
                                    "text": "Some Text",
                                    "attr": {
                                        "id": "child3",
                                        "class": [
                                            "child-div",
                                            "child3"
                                        ]
                                    },
                                    "children": []
                                }
                            ]
                        }
                    ]
                }
            ]
        }
    ]
}
```

## TODO:
- Add Documentation.

## License
MIT

See [license.md](license.md) for details.
