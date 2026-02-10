# Terraria Plugins [[Download DoombubblesPlugins.zip]](https://github.com/doombubbles/TerrariaPlugins/releases/latest/download/DoombubblesPlugins.zip)

A set of plugins for use with [TerrariaPatcher](http://forums.terraria.org/index.php?threads/24615/) in 1.4.5+, many of which recreate features from QoL mods I usually use in tModLoader 1.4.4

## Installation

Follow the instructions in the [TerrariaPatcher Forum Post](http://forums.terraria.org/index.php?threads/24615/) to install it. When running it, you will need to keep the "Plugin support (loads from \Plugins\*.cs)" checkbox enabled.

Then, go to the [latest release](https://github.com/doombubbles/TerrariaPlugins/releases/latest) and download [DoombubblesPlugins.zip](https://github.com/doombubbles/TerrariaPlugins/releases/latest/download/DoombubblesPlugins.zip).
Unzip it and put the contents into your TerrariaPatcher Plugins folder. You should now see them in the plugins list and be able to enable/disable them.

## Settings

Settings/hotkeys are stored in `Plugins.ini` like other TerrariaPatcher plugins. Settings for my plugins will update in game automatically if the file is edited.

For changing hotkeys, [see here for key names](https://docs.monogame.net/api/Microsoft.Xna.Framework.Input.Keys.html).
To use modifiers, put them before the key name with a comma like `Alt,Shift,OemPipe`

<details>

<summary>Example Default Settings</summary>

```ini
[DryadSeeds]
BloomConditions = true
[HelpfulHotkeys]
AutoRecall = Home
QuickBuffFavoritedOnly = Z
DashHotkey = X
DisableDoubleTapDash = false
SwapArmorVanity = None
SwapArmorInventory = None
SwapArmorInventorySlot1 = 29
SwapArmorInventorySlot2 = 39
SwapArmorInventorySlot3 = 49
SwapHotbar = None
CycleAmmo = OemPeriod
AutoCycleAmmo = false
ToggleAutoCycleAmmo = Shift,OemPeriod
StackToNearbyChests = None
RulerHotkey = None
SwitchFrameSkipMode = None
ToggleAutoPause = Pause
QuickUseItem10 = None
QuickUseItem11 = None
QuickUseItem12 = None
QuickUseItem13 = None
QuickUseItem14 = None
QuickUseItem15 = None
QuickUseItem16 = None
QuickUseItem17 = None
QuickUseItem18 = None
QuickUseItem19 = None
SwapAccessoryVanity1 = None
SwapAccessoryVanity2 = None
SwapAccessoryVanity3 = None
SwapAccessoryVanity4 = None
SwapAccessoryVanity5 = None
SwapAccessoryVanity6 = None
SwapAccessoryVanity7 = None
SwapAccessoryVanityAll = None
[PermaAmmo]
RequiredCount = 9999
ThrownRequiredCount = 999
[PermaBuffs]
ItemRequiredCount = 30
StationRequiredCount = 1
CumulativeTotal = false
AllowedItemBuffs = [1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,26,71,73,74,75,76,77,78,79,104,105,106,107,108,109,110,111,112,113,114,115,116,117,121,122,123,124,206,207,376,25,257]
StationBuffs = {"487":29,"966":87,"1859":89,"2177":93,"2999":150,"148":86,"3117":157,"1431":158,"3198":159,"3750":192,"4276":215,"3814":348}
[WhipBuffStacking]
WorkingBuffs=[308,311,312,314,365]
```

</details>

## Plugins List

### Whip Buff Stacking

Makes Whips' self-buffs no longer be removed when using another whip. This includes the Cobwhip Buff (Spider), Snapthorn Buff, Coolwhip Buff (Snowflake), Durendal Buff, and Dark Harvest Buff.

Note that still only 1 tag can be applied per enemy at a time

### Minion Local I-Frames

Changes all minion projectiles that use static immunity to instead use local immunity.
This affects Baby Slimes, Vampire Frogs, Imp Fireballs, Mini Spiders, Retanimini/Spazmamini, and Tempests.

### Perma Buffs

Stacks of 30+ of a buff potion in your inventory / piggy bank / void bag etc. will cause you to permanently have their buff active.
Having a stack of 1+ of a buff station such as the Crystal Ball will also permanently apply its buff. Required amounts are configurable.
There's also a `CumulativeTotal` setting (default false) to allow the 30 total for a buff to be reached across multiple different items/stacks.

Well Fed buffs are special cased to not override each other, so you could keep a stack of lesser food items to be permanently Well Fed,
then still eat a greater food item to be temporarily Exquisitely Stuffed

### Perma Ammo

When firing using a stack of ammo that's at 9999, it won't decrement and instead will stay at 9999. Required amount is configurable.
Also applies to consumable ranged weapons, which have their own configurable setting `ThrownRequiredCount` which defaults to 999.

### Dryad Seeds

Makes the Dryad sell herb seeds. By default, seeds are only sold when their blooming conditions are met, but this can be changed with the `BloomConditions` setting.

- Daybloom - Daytime (4:30 AM to 7:29 PM)
- Moonglow - Nighttime (7:30 PM to 4:29 AM)
- Blinkroot - At random (Moon Phase is not Gibbous/Cresecent)
- Deathweed - Blood Moon or Full Moon at nighttime
- Waterleef - Rain
- Firebloosom - Sunset (3:45 PM to 7:30 PM) unless it is raining
- Shiverthorn - At random (Moon Phase is Gibbous/Cresecent)

### Guns Minimum Knockback

Simply makes all guns (bullet shooting weapons) have at least 1 base knockback so they can receive more prefixes, such as Unreal.

### Helpful Hotkeys

Adds a number of hotkeys based on the [Helpful Hotkeys](https://github.com/JavidPack/HelpfulHotkeys) mod for tModLoader by [JavidPack](https://github.com/JavidPack)

#### Auto Recall (Default: Home)

Automatically uses a Magic Mirror / Recall Potion / Cellphone etc. from your inventory.

#### Quick Buff Favorited Only (Default: Z)

Standard quick buff but only including favorited items

#### Quick Use Item Hotkeys (Defaults: None)

10 configurable hotkeys for quick using the items in slots [10 - 19](InventorySlots.jpg) (top row of inventory, directly below hotbar, visually the 11th through 20th slots)

#### Dash Hotkey (Default X)

Activates the player's dash, if they have one. Additionally, there's a `DisableDoubleTapDash` setting (default false).

#### Swap Armor with Inventory (Default: None)

Swap your equipped armor with items in your inventory. By default, uses [slots with ids 29/39/49](InventorySlots.jpg) which form the bottom right 1x3 column of slots in your inventory.

#### Swap Armor with Vanity (Default: None)

Swap your equipped armor with the armor in your vanity.

#### Swap Hotbar (Default: None)

Swaps the items in your hotbar with the items in the top row of your inventory.

#### Swap Accessories with Vanity (Defaults: None)

7 configurable hotkeys for swapping specific accessories with the one in their vanity slot, +1 for swapping all.
Note that in pre-hardmode master mode, you will have slots 1-5 and 7, not 1-6.

#### Cycle Ammo (Default: Period)

Cycles the positions of the items in the ammo slots.

#### Auto Cycle Ammo (Default: Shift+Period)

Toggles a mode where ammo will be automatically cycled after each shot.

#### Stack to Nearby Chests (Default: None)

Activates the Quick/Similar Stack to Nearby Chests button

#### Ruler Hotkey (Default: None)

Toggles the block measurement ruler.

#### Toggle Auto Pause (Default: Pause/Break)

Toggles the autopause setting.

#### Switch Frame Skip Mode (Default: None)

Cycles Frame Skip between On/Subtle/Off