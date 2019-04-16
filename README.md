# Music Score Generator (web)
Configurable sheet music generator

It is written y C# with a minimalistic one-page ASP.NET website.

## Screenshots
<img src="screenshots/main.png">
<img src="screenshots/sheet.png">

## Dependencies

The application allows you to generate the lilypond script to be externally rendered or generate the PDF, for which you need to have <a href="http://lilypond.org">lilypond</a> installed.

The path to lilypond executable must be set in the web.config "lilypond.exec" app setting.
