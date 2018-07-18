---
id: unit_test
title: Unit Test
sidebar_label: Unit Test
---

## Mocking Classes

## Test
Testing highly enforced in neo-sharp. Your code can be rejected if you haven't written tests, even if it works perfectly.

Unit testing is also useful to prevent that future changes in other parts of the code affect the behavior of other features.

Since our code uses mostly Interfaces instead of concrete implementations, is very easy to mock the behavior of a class, making it easy to test only the particular parts of the code you are interested in.

NEO-Sharp aims for 90% of test coverage, and we use tools like travis and codecov to ensure this coverage.

## Code Coverage