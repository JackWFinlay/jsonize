# jsonize
Convert HTML to JSON. Currently available for .NET Framework 4.6, .NET Standard Library 1.6, and .NET Core 1.0. May be compatible with lower versions, any help porting backwards would be great.

Try it out: http://jsonize.co

Get the NuGet package: https://www.nuget.org/packages/JackWFinlay.Jsonize/

## Usage

Use the using statement:
```C#
using JackWFinlay.Jsonize;
```

An example to get the site "http://jackfinlay.com" as a JSON string:

```C#
private static async Task<string> Testy(string q = "")
{
    using (var client = new HttpClient())
    {
        string url = @"http://jackfinlay.com";

        HttpResponseMessage response = await client.GetAsync(url);

        string html = await response.Content.ReadAsStringAsync();
        Jsonize jsonize = new Jsonize(html);

        return jsonize.ParseHtmlAsJsonString();
    }
}
```

Alternatively, get the response as a Newtonsoft.Json JObject:

```C#
return jsonize.ParseHtmlAsJson();
```

You can control the output with a JsonizeConfiguration object, which is passed as a parameter to the parsing methods:

```C#
...
JsonizeConfiguration jsonizeConfiguration = new JsonizeConfiguration
{
    NullValueHandling = NullValueHandling.Include,
    EmptyTextNodeHandling = EmptyTextNodeHandling.Ignore
};

return jsonize.ParseHtmlAsJsonString(jsonizeConfiguration);
```

Results are in the form:
```JSON
{
    "node":"Node type e.g. Document, Element, or Comment",
    "tag":"If node is Element this will display the tag e.g p, h1 ,div etc.",
    "text":"If node is of type Text, this will display the text in that node.",
    "attr":{
                "name":"value",
                "name":"value",
                "class": []
            },
    "child":[
                {
                    "node":"Node type e.g. Document, Element, or Comment",
                    "tag":"If node is Element this will display the tag e.g p, h1 ,div etc.",
                    "text":"If node is of type Text, this will display the text in that node.",
                    "child": []
                },
                ...
            ]
```

## Example:
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
  "node": "Document",
  "child": [
    {
      "node": "Comment",
      "text": "<!DOCTYPE html>"
    },
    {
      "node": "Element",
      "tag": "html",
      "child": [
        {
          "node": "Element",
          "tag": "head",
          "child": [
            {
              "node": "Element",
              "tag": "title",
              "child": [
                {
                  "node": "Text",
                  "text": "Jsonize"
                }
              ]
            }
          ]
        },
        {
          "node": "Element",
          "tag": "body",
          "child": [
            {
              "node": "Element",
              "tag": "div",
              "attr": {
                "id": "parent",
                "class": [
                  "parent-div"
                ]
              },
              "child": [
                {
                  "node": "Element",
                  "tag": "div",
                  "attr": {
                    "id": "child1",
                    "class": [
                      "child-div",
                      "child1"
                    ]
                  },
                  "child": [
                    {
                      "node": "Text",
                      "text": "Some Text"
                    }
                  ]
                },
                {
                  "node": "Element",
                  "tag": "div",
                  "attr": {
                    "id": "child2",
                    "class": [
                      "child-div",
                      "child2"
                    ]
                  },
                  "child": [
                    {
                      "node": "Text",
                      "text": "Some Text"
                    }
                  ]
                },
                {
                  "node": "Element",
                  "tag": "div",
                  "attr": {
                    "id": "child3",
                    "class": [
                      "child-div",
                      "child3"
                    ]
                  },
                  "child": [
                    {
                      "node": "Text",
                      "text": "Some Text"
                    }
                  ]
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
- Port to .Net 4.6.
- Add support to directly pass in a URL.
- ~~Remove formatting issues such as empty child arrays on Script tags with no content.~~ Fixed in 1.0.3
- Add Documentation.
- ~~Change class attribute to array rather than space separated.~~ Fixed in 1.0.2
