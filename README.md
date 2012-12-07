Alfero's Northwind Project
================================

This project is a demonstration of several technologies

* Entity Framework Code-First
* ASP.NET WebPages
* ASP.NET Web API
* Knockout
* T4 Scaffolding
* Twitter Bootstrap
* JQuery

Entity Framework
----------------

Here I wanted to find out if I can use Entity Framework Code First without modifying the database. 
Evidently, this is very possible for an application of this size. There are some minor hacks I had to employ
around code-generated unique keys, but I'm sure there is an elegant way to handle those.

ASP.NET Web Pages
-----------------

While discussing with a co-worker I came up with an idea 
to put together a project that demonstrates the feasibility of building a functional application with just
ASP.NET Web Pages and ASP.NET Web API controllers. Very quickly it turned out there were a lot of repetitive steps
that could be automated. That's where T4 Scaffolding came in.

Knockout
--------

I simply fell in love with the concept, but wasn't sure if I could build a scalable real-world application with it.
Again, I'm not an expert here, but a little research brought me thus far. What I found interesting was dealing with 
the Order-OrderDetail relationship.

T4 Scaffolding
--------------

Initially I used T4 Toolbox to build and run the templates that generate Models, Controllers, and Views.
However, it became very clear to me that Scaffolding was a better approach:

* There are a number of built-in powershell functions and scaffolders to get you started
* There are a number of helpful ENVDTE extensions for discovering code
* Does not require the referenced project to be compiled first (as I was using reflection previously).


Twitter Bootstrap
-----------------

I now prefer this framework over jQuery UI. Just me.

jQuery
------

I don't think I have to explain this one.

Getting Started
===============

Just open the project in Visual Studio and run.

This Project Sucks?
===================

Probably so, but all comments are welcome. I had no real incentive to demonstrate any other technology besides the ones listed above.
I know I don't have any tests in this project, but that wasn't the point. Feel free to add if you're so inclined.
This is a very rough implementation from a non-expert, so I'm pretty sure this can be improved (especially the scaffolding code - it's super ugly, I know).

Roadmap/Ideas
=============

* Polish up application functionality
* Polish up scaffolders
* Add Client-side validation with JQuery.Validate
* Contribute extensions to the T4 Scaffolding core
* Create a new WebPages/Knockout Scaffolding nuget package (help?) for scaffolding plain razor pages with Knockout bindings