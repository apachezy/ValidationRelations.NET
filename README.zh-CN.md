# PropertyComparison Validation for .NET

[![English](https://img.shields.io/badge/Language-English-blue)](README.md)
[![中文](https://img.shields.io/badge/Language-中文-red)](README.zh-CN.md)

一个轻量级、高性能的 **.NET DataAnnotations 扩展验证库**。

该库提供了一组可复用的 **属性关系验证 Attribute**，例如：

* 属性之间比较（`GreaterThan`、`LessThan`、`EqualTo` 等）
* 区间验证（`Between`、`Within`）
* 时间规则（`GreaterThanNow`、`BetweenNowAnd`）
* 条件验证（`RequiredIf`、`RequiredIfAny` 等）

该库可以直接用于：

* ASP.NET Core
* WPF / MVVM
* WinForms
* 任何使用 `System.ComponentModel.DataAnnotations` 的 .NET 项目

---

# 特性

• 支持属性之间的值比较
• 支持 **嵌套属性路径**（例如 `Order.CreateTime`）
• **Null 安全访问**（等价于 `obj?.Order?.CreateTime`）
• 支持不同数值类型比较（`int`、`long`、`decimal` 等）
• 支持枚举类型比较
• 高性能 **表达式 Getter 缓存（Compiled Expression Cache）**
• 高性能 **比较委托缓存（Comparison Delegate Cache）**
• 与标准 **DataAnnotations 验证流程**完全兼容

---

# 安装

目前可以通过源码方式集成。

克隆仓库：

```bash
git clone https://github.com/apachezy/ValidationRelations.NET.git
```

然后将 `ValidationRelations` 项目加入你的解决方案即可。

---

# 基本用法

示例模型：

```csharp id="sm3t2l"
public class TimeRange
{
    public DateTime Start { get; set; }

    [GreaterThan(nameof(Start))]
    public DateTime End { get; set; }
}
```

验证规则：

```id="2mgy1g"
Start < End
```

---

# 区间验证

```csharp id="0h3dsm"
public class RangeModel
{
    public int Min { get; set; }

    public int Max { get; set; }

    [Between(nameof(Min), nameof(Max))]
    public int Value { get; set; }
}
```

规则：

```id="fc2kpk"
Min <= Value <= Max
```

---

# 嵌套属性比较

支持嵌套属性路径。

```csharp id="q0u78e"
public class Shipment
{
    public Order Order { get; set; }

    [GreaterThan("Order.CreateTime")]
    public DateTime ShipTime { get; set; }
}
```

---

# 时间规则

```csharp id="w95fbe"
[GreaterThanNow]
public DateTime ExpireTime { get; set; }

[LessThanNow]
public DateTime CreatedTime { get; set; }

[BetweenNowAnd(nameof(ExpireTime))]
public DateTime ExecuteTime { get; set; }
```

---

# 条件验证

根据其它属性决定是否必填。

```csharp id="0c3pjn"
[RequiredIf(nameof(Type), OrderType.Special)]
public string SpecialCode { get; set; }
```

可用规则：

```id="sbxxgm"
RequiredIf
RequiredIfNot
RequiredIfAny
RequiredIfAll
```

---

# 可用 Attribute

### 属性比较

```id="96vyh4"
GreaterThan
GreaterOrEqual
LessThan
LessOrEqual
EqualTo
NotEqualTo
DistinctFrom
```

### 区间验证

```id="mye7h5"
Between
NotBetween
Within
BetweenNowAnd
```

### 时间验证

```id="pjf41r"
GreaterThanNow
LessThanNow
```

### 多值比较

```id="hyl5mz"
EqualToAny
AllowedValues
```

### 条件验证

```id="8yb64q"
RequiredIf
RequiredIfNot
RequiredIfAny
RequiredIfAll
```

---

# 项目结构

```id="uvvrcv"
Utilities.Validation
│
├─ PropertyComparison
│   ├─ Attributes
│   ├─ Infrastructure
│   └─ Comparison helpers
│
└─ Conditional
    └─ 条件验证 Attribute
```

---

# License

MIT License

---

# Contributing

欢迎提交 Issue 或 Pull Request。

