---
id: intermediate_guide
title: Intermediate Guide
sidebar_label: Intermediate Guide
---

This guide is intended to help people with little or no Blockchain knowledge. If you have already contributed with Blockchain technologies before, you can skip to [advanced guide](/advanced_guide).

This guide covers:
1. Blockchain basics;
2. Development concepts used by this project;
3. Unit Testing;

## Blockchain Basics

To contribute with this project, it's recommended that you have some basic knowledge about Blockchain and related technologies. Below is a list of what we consider the most basic knowledge in this area. Follow the links provided in this article to ensure you have a solid understanding about it.
If you find any mistakes or unclear instruction, please [contact the team or submit a pull request](/basic_guide).

### Block and BlockHeader

 The blocks are used to store the data of the whole network, such as transactions or assets, and every block has it's own header to store meta-data about the block itself. A more detailed explanation can be found [here](/block).

### Genesis block and the Blockchain

The blockchain is the logical structure, connecting Blocks in a one-way linked list, and every new block depends on it's previous block. The first block is called Genesis block. It's in this block that NEO and GAS is born. A more detailed explanation can be found [here](/Blockchain).

### Cryptographic Hashing functions - SHA256 and RIPMED160

A hashing function can be applied to data resulting in a fixed size output. In NEO, we use 2 known algorithms to create hashes: SHA256 and RIPMED160. The first will result in a 256 bits (32 bytes) message, while the second will generate a 160 bits (20 bytes) message.
In NEO, we use 32-byte hashes for computing Block and Transaction Hashes, and 20-bytes hashes for address generation. In code these types are represented in UInt256 and UInt160 respectively. A more detailed information can be found [here](/Hash).

### Unspent Transaction Output (UTXO)

NEO uses the UTXO model, meaning that, to make a transaction, you must reference a transaction that you received in the past, that was not used yet. An address balance is the sum of all these 'unspent' transactions. A more detailed explanation can be found [here](/UTXO).

## Development concepts used by this project

This project relies on a few particular C# functionalities. A brief description can be found [here](/Development).
