# Unity 2D Tower Defense

一个使用 **Unity 2D** 从零搭建的塔防小游戏项目。  
项目实现了塔防游戏的核心循环，包括 **路径系统、波次刷怪、自动攻击、防御塔升级、敌人类型系统以及基地生命机制**。

该项目以模块化结构编写，便于扩展新的塔、防御机制或敌人类型。

---

# 项目特性

### 路径系统
敌人按照预设路径移动。

- 支持多节点路径
- 使用 `Vector3.MoveTowards()` 实现移动
- 路径可在 Unity Scene 中直接编辑

---

### 波次刷怪系统

`WaveSpawner` 负责控制敌人的生成。

每一波可以配置：

- Enemy Prefab
- 敌人数量
- 刷新间隔
- 速度倍率
- 敌人类型

示例：

```
Wave 1
Enemy Type : Normal
Count : 5
Spawn Interval : 1s
Speed Multiplier : 1

Wave 2
Enemy Type : Fast
Count : 8
Spawn Interval : 0.8s
Speed Multiplier : 1.3
```

---

### 防御塔系统

防御塔会自动寻找范围内的敌人并进行攻击。

核心功能：

- 自动索敌
- 自动射击
- 子弹追踪
- 伤害结算

主要参数：

```
Attack Range
Fire Rate
Damage
Enemy Layer
```

---

### 塔升级系统

每个防御塔可以进行多次升级。

升级可以提升：

- 攻击伤害
- 攻击速度
- 攻击范围

升级配置示例：

```
Level 1
Cost : 80
Damage +1
FireRate +0.2
Range +0.2

Level 2
Cost : 130
Damage +2
FireRate +0.3
Range +0.3
```

升级通过点击塔触发。

---

### 敌人类型系统

通过 `EnemyTypePreset` 控制不同敌人的属性。

目前支持类型：

```
Normal
Fast
Tank
Shielded
```

不同类型会改变：

- 生命值
- 移动速度
- 护盾
- 护甲
- 奖励金币

---

### 基地生命系统

当敌人到达终点时：

- 基地生命值减少
- UI 血条更新

界面显示：

```
Base HP : 当前生命 / 最大生命
```

当生命为 0 时游戏结束。

---

### 经济系统

击杀敌人可获得金币：

```
Enemy Reward Gold
```

金币用于：

- 建造防御塔
- 升级防御塔

---

### UI HUD

当前 UI 显示：

```
Gold
Base HP
Wave
GameOver / Victory
```

UI 使用事件驱动方式更新。

---

# 项目结构

```
Assets
 ├─ Scripts
 │   ├─ Core
 │   │   GameManager
 │   │   Economy
 │
 │   ├─ Pathing
 │   │   WaypointPath
 │   │   EnemyMover
 │
 │   ├─ Enemies
 │   │   EnemyHealth
 │   │   EnemyTypePreset
 │
 │   ├─ Combat
 │   │   Tower
 │   │   Bullet
 │
 │   ├─ Spawning
 │   │   WaveSpawner
 │
 │   ├─ Building
 │   │   BuildSpot
 │   │   TowerUpgrade
 │
 │   └─ UI
 │       HUD
 │       BaseHealthBarView
 │
 └─ Docs
     UnitySetup.md
```

---

# Unity 场景配置

## 1 创建路径

在 Scene 中创建：

```
Path
 ├─ P0
 ├─ P1
 ├─ P2
 └─ P3
```

敌人将从 `P0` 移动到 `P3`。

---

## 2 设置刷怪器

在 `WaveSpawner` 中配置：

```
waves[]
```

每一波设置：

```
Enemy Prefab
Count
Spawn Interval
Speed Multiplier
Enemy Type
```

---

## 3 设置敌人

Enemy Prefab 需要包含：

```
EnemyHealth
EnemyMover
EnemyTypePreset
Collider2D
```

示例：

```
Max Health : 5
Shield : 0
Armor : 0
Reward Gold : 10
```

---

## 4 建塔点

创建对象：

```
BuildSpot
```

添加：

```
Collider2D
BuildSpot Script
```

点击后生成防御塔。

---

## 5 防御塔配置

Tower Prefab 需要包含：

```
Tower
TowerUpgrade
Collider2D
```

示例参数：

```
Attack Range : 2.5
Fire Rate : 1
Damage : 3
```

---

## 6 基地血条 UI

Canvas 中创建：

```
BaseHealthBar
 ├─ Fill (Image)
 └─ ValueText (TextMeshPro)
```

然后在 `BaseHealthBarView` 中绑定：

```
Fill Image
Value Text
```

---

# 运行项目

1 使用 **Unity Hub** 打开项目  
2 打开 `MainScene`  
3 点击 **Play**

游戏流程：

```
刷怪 → 建塔 → 攻击 → 击杀 → 获得金币 → 升级塔 → 防守基地
```

---

# 可扩展方向

该项目结构支持继续扩展：

- 新防御塔类型
- 技能塔
- Boss敌人
- 对象池优化
- ScriptableObject 配置系统
- 关卡系统

---

# 技术栈

```
Unity 2022.3 LTS
C#
Unity 2D Physics
TextMeshPro
```

# 作者

GitHub  
LCZXXX
