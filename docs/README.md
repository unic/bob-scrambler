<div class="chapterlogo">![Scrambler](Scrambler.jpg)</div>
# Scrambler

Scrambler provides web-services which can be called via PowerShell to
perform actions inside Sitecore.

## Authorization
The APIs can be called either if one is logged in as admin in Sitecore, or
when the HTTP call comes from the local machine.


## API Endpoints
Scrambler provides different API Endpoints, which are documented here.

| Endpoint | Description |
| --- | --- |
| /bob/updateDatabase | Performs an "update database". This means that all serialized items from the default location, not existing in the database will be added.  |
| /bob/updateIndex | _Not yet implemented_ |
| /bob/resetAdmin | Resets the password of the admin user to "b" |
