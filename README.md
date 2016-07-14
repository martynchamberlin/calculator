CalculatorApp
=============

This is a calculator assignment for a 4000-level Windows Programming class. I created this repository because I've found that having the ability to roll back to a previous version of a Visual Studio project is invaluable. There is something about the clunkiness of Windows Forms that engenders obfuscated control layouts and if you're trying to have a tight design (very common in Objective-C applications but I fear less so in C#) then this is an issue. Hence version control.

This is a nice calculator. It allows for more than two operands to be entered prior to the overall calculation. When I built something similar 18 months ago I used the Shunting Yard Algorithm and Reverse Polish Notation. If I remember correctly it took somewhere between 100-200 lines of code. But, tonight a friend showed a nifty trick that saves one from having to go through all of this hassle. 

	String input = "5 + 4 / 3 - 2";
	var answer = new DataTable().compute( input, null);
	outputBox.Text = Convert.ToString( answer );

Of course, in real life (or even as in my academic assignment here) you would want to enclose that in a try-catch block. The only problem with using a `DataTable` object is that it does not perform exponents, which was part of my assignment. I made a special case scenario for this. 

## A Note on Design

You'll notice this application is colorful. That's because the assignment was to be for 6th graders to use. I dislike how bland most C# applications are (particularly in academia) and so I welcomed this opportunity to build something colorful.

<img src="https://raw.githubusercontent.com/martynchamberlin/calculator/master/screenshot.png" width="300"/>

## What's up with those Mac OS X file names?

I have a deep distrust of the Windows operating system. Whenever I log into it, I tend to back up every keystroke into Dropbox. Since I run Windows virtually on my Macbook Pro, switching back to native Mac OS X is a swope of 4 fingers. I do as little in Windows as I possibly have to. So that's why this project, although specific to the Windows operating system, has Mac OS X files scattered throughout. Amusing that the default `.gitignore` configurations that GitHub provides for Visual Studio projects does not account for this. They should appreciate that a disproportionate number of GitHub users are likely to be VM'ing into Windows from a superior operating system. 

## Usage

You are welcome to download, run, and learn from my code. You are also welcome to criticize it and improve it. 


