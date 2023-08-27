# [SharedUtils](../README.md)/Memory
Provides a number of utilities for managing memory structures in C#.

# Table of Contents
---
- [Memory/Bytes](Bytes.md)
> Provides utility functions for managing memory structures in C#.
> These are accessed through the static `Bytes` class and an assortment
> of extension methods on `byte[]` and `T where T: struct`.
- [Memory/OverallocatingArray](OverallocatingArray.md)
> This provides a simple implementation of an array that scales in powers
> of two. This is useful for quickly growing lists, such as those used in
> the Compensation VR creation tools.