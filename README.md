# Take Home Coding Exercise

*Problem*: 
You must get fully dressed before leaving the house

*Rules*:
* At the start, assume you have PJ’s on
* Only 1 piece of each item of clothing may be put on
* You cannot put on socks when it is hot
* You cannot put on jacket when it is hot
* Socks must be put on before footwear
* Shirt must be put on before headwear and jacket
* Pants must be put on before footwear
* Pajamas must be taken off before anything can be put on
* You cannot leave the house until all items of clothing are on (except socks and a jacket when it’s hot)
* If an invalid command is issued, please respond with “fail”

*Input*: 
Temperature type (`HOT`|`COLD`) and a comma separated list of numeric commands

|Command  |    Description      |    Hot Response    |    Cold Response|
|---|---|---|---|
|1        |    Put on footwear  |    “sandals”       |    “boots”|
|2        |    Put on headwear  |    “sunglasses”    |    “hat”|
|3        |    Put on socks     |    fail            |    “socks”|
|4        |    Put on shirt     |    “shirt”         |    “shirt”|
|5        |    Put on jacket    |    fail            |    “jacket”|
|6        |    Put on pants     |    “shorts”        |    “pants”|
|7        |    Leave house      |    “leaving house” |    “leaving house”|
|8        |    Take off pajamas |    “Removing PJs”  |    “Removing PJs”|

## Here are some example scenarios

### Successful
***
*Input*: `HOT 8, 6, 4, 2, 1, 7`

*Output*: `Removing PJs, shorts, shirt, sunglasses, sandals, leaving house`
***
*Input*: `COLD 8, 6, 3, 4, 2, 5, 1, 7`

*Output*: `Removing PJs, pants, socks, shirt, hat, jacket, boots, leaving house`
***
### Failure
***
*Input*: `HOT 7`

*Output*: `failure`
***
*Input*: `HOT 8, 6, 6`

*Output*: `Removing PJs, shorts, fail`
***
*Input*: `HOT 8, 6, 3`

*Output*: `Removing PJs, shorts, fail`
***
*Input*: `COLD 8, 6, 3, 4, 2, 5, 7`

*Output*: `Removing PJs, pants, socks, shirt, hat, jacket, fail`
***
*Input*: `COLD 8, 6, 3, 4, 2, 5, 1`

*Output*: `Removing PJs, pants, socks, shirt, hat, jacket, boots, fail`
***
*Input*: `COLD 6`

*Output*: `fail`
***
