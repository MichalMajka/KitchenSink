# KitchenSink

Shows how to model different UI patterns in JSON:

- String
   - Text
   - Password
   - Textarea
   - Markdown
   - Html
   - Datepicker
   - Url
   - Redirect

- Number
   - Integer
   - Decimal
   - Button
   - Map

- Boolean
   - Checkbox
   - Button
   - Togglebutton

- Array
   - Radio
   - Dropdown
   - Radiolist
   - Multiselect
   - Table
   - Chart

## Running

To try the application, first install Visual Studio 2015 Community and Starcounter. 

Download the code from this repo, build it and run it in Visual Studio. Then, go to `http://localhost:8080/KitchenSink` in your web browser.

## Requirements

Requires Starcounter version 2.2.1.2903

## Video

Intended for 13 October 2015 webinar: http://starcounter.io/video-expressing-your-ui-in-json-plain-data-binding-advanced-data-binding/

## Screenshot

![](https://raw.githubusercontent.com/StarcounterSamples/KitchenSink/master/screenshot.png)

## Excercises

### 1. Change binding feedback event

From:

```cs
<input type="text" value="{{model.Name$::change}}" placeholder="Name">
```

To:

```cs
<input type="text" value="{{model.Name$::input}}" placeholder="Name">
```

## Testing

### Prepare your environment

Before running the steps, you need to:

- Download and install Visual Studio 2015 to run the tests
- Download and install Java, required by Selenium Standalone Server
- Download Selenium Standalone Server and the drivers (Microsoft WebDriver (Edge), Google ChromeDriver (Chrome) and Mozilla GeckoDriver (Firefox)) using the instructions at http://starcounter.io/guides/web/acceptance-testing-with-selenium/#install-selenium-standalone-server-and-browser-drivers
- Add path to the folder with drivers to system path on your computer

### Run the test (from Visual Studio)

1. Start Selenium Remote Driver: `java -jar selenium-server-standalone-3.*.jar`
2. Open `KitchenSink.sln` in Visual Studio and enable Test Explorer (Test > Window > Test Explorer)
3. You need to install NUnit 3 Test Adapter in VS addon window in order to see tests in Test Explorer window
3. Start the KitchenSink app
4. Press "Run all" in Test Explorer
   - If you get an error about some packages not installed, right click on the project in Solution Explorer. Choose "Manage NuGet Packages" and click on "Restore".

### Run the test (from command line)

1. Start Selenium Remote Driver: `java -jar selenium-server-standalone-3.*.jar`
2. Build the solution (build.bat)
3. Run the KitchenSink app (run.bat)
4. Start the KitchenSink.Test runner (test.bat)

### How to release a package

1. Install [Node.js](https://nodejs.org/).
2. Run `npm install` to install all dependencies.
3. Run `grunt package` to generate a packaged version. You can use `grunt package:minor`, `grunt package:major`, `grunt package --setversion=1.0.0-develop.0`, etc. as [grunt-bump](https://github.com/vojtajina/grunt-bump)

## License

MIT
