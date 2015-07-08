<div class="chapterlogo">![Scrambler](Scrambler.jpg)</div>
# Scrambler

Scrambler provides web-services which can be called via PowerShell to
perform actions inside Sitecore.

## Installation
To install Scrambler simply install one off these packages:
If you are using Sitecore 7.1 or lower use **Unic.Bob.Scrambler.Core**,
if you are using Sitecore 7.2 or higher use **Unic.Bob.Scrambler.ContentSearch**.

## Authorization
The APIs can be called either if one is logged in as admin in Sitecore, or
when the HTTP call comes from the local machine.


## API Endpoints
Scrambler provides different API Endpoints, which are documented here.

| Endpoint | Description |
| --- | --- |
| /bob/updateDatabase | Performs an "update database". This means that all serialized items from the default location, not existing in the database will be added.  |
| /bob/fullPublish | Performs a full publish. |
| /bob/rebuildIndex?indexes=index1,index2 | Performs an index rebuild on the specified indexes. |
| /bob/disableIndexing | Disables indexing of Sitecore items. |
| /bob/reEnableIndexing | Enable indexing of Sitecore items, but only if it was already enabled when Sitecore started. |
| /bob/resetAdmin | Resets the password of the admin user to "b" |
