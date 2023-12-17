# Implementation

## Server Connection.
I use a custom library I built for a Rest API, and I use the GET function to retrieve JSON data from the server.
[Library link](https://github.com/Anoop114/HelperPackages)
---
## Download All Images.
After the JSON data has been obtained, download the images contained in the JSON file by looping through the data.

Note: I also look for edge cases, such as when a user receives an error if the url is broken or if JSON data is not entered. There are a few more cases as well, such as incorrect image urls and unsuccessful image downloads, but I am only looking into a small number of these in this brief amount of time.

---

## Create Checker Pattern

After downloading images to the user's local computer, I use ``Application.persistentDataPath`` to retrieve the image and load it back into the display. To do this, I create a loop that ``instantiates`` images at runtime and lets the ``canvas layout group`` handle image alignment.

## Additional
To improve the UI popup and close animation by code, use [DOTween](https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676).