<h1 align="center">Achievement System</h1>
<p align="center">
<a href="https://openupm.com/packages/com.studio23.ss2.achievementsystem/"><img src="https://img.shields.io/npm/v/com.studio23.ss2.achievementsystem?label=openupm&amp;registry_uri=https://package.openupm.com" /></a>
</p>

Introducing an Achievement System package for Unity, enabling the creation and management of achievements without coding.

## Table of Contents

1. [Installation](#installation)
2. [Usage](#usage)
   - [Getting Started](#Getting-Started)
   - [Using the Achievement System](#Using-the-Achievement-System)
3. [Extensions](#Extensions)

## Installation

### Install via Git URL

You can also use the "Install from Git URL" option from Unity Package Manager to install the package.
```
https://github.com/Studio-23-xyz/com.studio23.ss2.achievementsystem.git#upm
```

## Usage

### Getting Started

To start using the Achievement System. You need to take a few setup stepts

1. Click on the 'Studio-23' available on the top navigation bar and navigate to "Achievement System > ID Table Generator"

2. A new window will appear in-front of you. Fill it up with the names of your Achievements.

3. It will generate an achievements.cs class with static string properties that you can use when calling the UnlockAchievement() method

4. Once you're done,Again on the top navigation bar and navigate to "Achievement System > ID Table Map Generator". Now you need to create a map for your Preferred Provider (Steam, Xbox, etc.)

5. This IDMap needs to be assigned on your provider Script.

6. Install your Preferred provider extention package.

### Using the Achievement System

1. Create an empty gameobject. 

2. Attach The Achievement System Script to it. 

3. Create your own provider or install one and add it to the same gameobject.

4. Simply call this following method to unlock an Achievement

```Csharp
   AchievementSystem.instance.UnlockAchievement(AchievementData achievementData)
```


## Extensions

There are two extensions available for In-Game Achievement System that adds support for Xbox and Steam platforms separately. Please ensure that this base package is installed if you want to use either of them. And that only one of the extension packages can be used at a time. 