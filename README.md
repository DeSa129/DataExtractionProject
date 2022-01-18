# DataExtractionProject

hello,

Would like to share some thought process while doing this project,

To start off - to avoid first page load I decided to generate URL in project with flight outbound day, inbound day and number of persons (which is important to generate taxes). 
by doing this, I avoided at least 1-page load for each flight combination.

Since homework is asking to get all possible flight combinations, means I cannot select all flight in loaded URL, first I need to find containers for outbound flights and inbound flights add them to lists, and using those lists I can generate fare flights

Second part of homework is asking to get taxes, which is in second section of flight purchase form.
That caused me some trouble, because I decided to make this project with HtmlAgilityPack library, which doesn't have browser engine (just lack of experience on my part).
Therefore, I could not select flights and go to next section (at least I didn't found a way, with HtmlAgilityPack, but I found a  way with selenium)
but after few manual tests I saw that taxes is constant per person so i just hard coded it, but I don't think it's the best solution.

I think ideal solution for this would be before getting flights, extract taxes percent or constant with one or two request (to check is it right) and then code formula for it.
That would save a lot of requests (because if you get taxes for each flight individually, that would at least double or triple the request number)

conclusion,

Was pretty fun project, I enjoyed it more than I thought I will, most of it was really intuitive, since I had decent  experience with HTML and webs it didn't feel that I'm doing something I don't know. If it's not too much to ask I would like to ask some feedback (anything that would help to improve)
if I would make project like this again, I would definitely use Selenium, it has all functionality which I was missing and would make project more complete :)
