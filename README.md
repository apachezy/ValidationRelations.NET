# PropertyComparison Validation for .NET

[![English](https://img.shields.io/badge/Language-English-blue)](README.md)
[![中文](https://img.shields.io/badge/Language-中文-red)](README.zh-CN.md)

A lightweight and high-performance **DataAnnotations validation extension** for .NET.

This library provides a set of reusable **property relationship validation attributes** such as:

* Property comparison (`GreaterThan`, `LessThan`, `EqualTo`, etc.)
* Range validation (`Between`, `Within`)
* Time rules (`GreaterThanNow`, `BetweenNowAnd`)
* Conditional validation (`RequiredIf`, `RequiredIfAny`, etc.)

It is designed to work with:

* ASP.NET Core
* WPF / MVVM
* WinForms
* Any project using `System.ComponentModel.DataAnnotations`

---

# Features

• Compare values between properties
• Support **nested property paths** (`Order.CreateTime`)
• **Null-safe property access** (`obj?.Order?.CreateTime`)
• Cross-type numeric comparison (`int`, `long`, `decimal`, etc.)
• Enum comparison support
• High-performance **compiled expression getter cache**
• High-performance **comparison delegate cache**
• Works with standard **DataAnnotations validation pipeline**

---

# Installation

Currently available as source integration.

Clone the repository and include the project in your solution:

```bash
git clone https://github.com/apachezy/ValidationRelations.NET.git
```

Or copy the `ValidationRelations` project into your solution.

---

# Basic Usage

Example model:

```csharp
public class TimeRange
{
    public DateTime Start { get; set; }

    [GreaterThan(nameof(Start))]
    public DateTime End { get; set; }
}
```

Validation rule:

```
Start < End
```

---

# Range Validation

```csharp
public class RangeModel
{
    public int Min { get; set; }

    public int Max { get; set; }

    [Between(nameof(Min), nameof(Max))]
    public int Value { get; set; }
}
```

Rule:

```
Min <= Value <= Max
```

---

# Nested Property Comparison

Nested property paths are supported.

```csharp
public class Shipment
{
    public Order Order { get; set; }

    [GreaterThan("Order.CreateTime")]
    public DateTime ShipTime { get; set; }
}
```

---

# Time Rules

```csharp
[GreaterThanNow]
public DateTime ExpireTime { get; set; }

[LessThanNow]
public DateTime CreatedTime { get; set; }

[BetweenNowAnd(nameof(ExpireTime))]
public DateTime ExecuteTime { get; set; }
```

---

# Conditional Validation

Require a property based on other properties.

```csharp
[RequiredIf(nameof(Type), OrderType.Special)]
public string SpecialCode { get; set; }
```

More rules:

```
RequiredIf
RequiredIfNot
RequiredIfAny
RequiredIfAll
```

---

# Available Attributes

### Property Comparison

```
GreaterThan
GreaterOrEqual
LessThan
LessOrEqual
EqualTo
NotEqualTo
DistinctFrom
```

### Range Validation

```
Between
NotBetween
Within
BetweenNowAnd
```

### Time Validation

```
GreaterThanNow
LessThanNow
```

### Multi-value Comparison

```
EqualToAny
```

### Conditional Validation

```
RequiredIf
RequiredIfNot
RequiredIfAny
RequiredIfAll
```

---

# Project Structure

```
Utilities.Validation
│
├─ PropertyComparison
│   ├─ Attributes
│   ├─ Infrastructure
│   └─ Comparison helpers
│
└─ Conditional
    └─ Conditional validation attributes
```

---

# License

MIT License

---

# Contributing

Issues and pull requests are welcome.

