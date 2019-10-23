# mThink
![Github issues](https://img.shields.io/github/issues/davidsonbrsilva/mThink.svg) ![Github forks](https://img.shields.io/github/forks/davidsonbrsilva/mThink.svg) ![Github stars](https://img.shields.io/github/stars/davidson-bruno/mThink.svg) ![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg) ![Status](https://img.shields.io/badge/status-stopped-green?style=flat-square)

This is an artificial intelligence project about the emergence of self-organized symbol-based communication in artificial creatures. Through it we are studying the role of grammar in the context of emerging languages. This is a problem based on the behavior of monkeys vervets found in Africa and the purpose of this system is to make them learn to communicate with each other to increase their chances of survival.

This system uses the reinforcement learning method. The agents of this system are monkeys and predators. At each interval in time, each monkey checks for predator within its range of vision. If there is, it sends the highest value signal to that predator you are viewing. Other monkeys that are in this signal radius receive it and automatically check if they are seeing any predators. If a predator is detected, they reinforce what they know about the signal they received and the predator they are seeing.

## Versions
Each version of mThink is like a different project. Currently only the mThink v3 version is supported because it is like a new project that takes the experiences of its previous versions to build a system concerned with the details of the problem and with features that extend the original version of the problem. It is the purpose of this version, for example, to implement the functionality of predator hunting and monkey escape.

### About [v3](https://github.com/davidsonbrsilva/mThink/tree/master/v3)
![version status](https://img.shields.io/badge/status-deprecated-red.svg)

In this version monkeys are displayed on the screen and the interaction among them can be viewed in a more user friendly way. The movement among them, albeit random, already uses Moore's neighborhood.

### About [v2](https://github.com/davidsonbrsilva/mThink/tree/master/v2)
![version status](https://img.shields.io/badge/status-deprecated-red.svg)

This version is a Windows Presentation Forms application where the monkeys are displayed on the screen and the interaction among them can be viewed in a more user friendly way. The movement among them, albeit random, already uses Moore's neighborhood.

### About [v1](https://github.com/davidsonbrsilva/mThink/tree/master/v1)
![version status](https://img.shields.io/badge/status-deprecated-red.svg)

This version is a console application that shows the interaction and communication among the monkeys. The movement is still random, but the symbols used by monkeys converge, showing that they learn to use the same language to refer to predators in the system, increasing their chances of survival.

## Thanks
Special thanks to Professor Leonardo Lana de Carvalho who presented me with the problem and supports the project.

## Contact
For more information, send an email to <davidsonbruno@outlook.com>.
