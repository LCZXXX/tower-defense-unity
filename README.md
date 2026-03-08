# Unity 2D Tower Defense

一个从零搭建的 **Unity 2D 塔防核心系统 Demo**。  
该项目用于展示游戏开发中的核心系统设计与代码结构，可作为 **Unity / 游戏开发实习项目作品**。

项目实现塔防游戏的基础循环：

Enemy Spawn → Path Movement → Tower Targeting → Shooting → Damage → Economy → UI

通过模块化脚本结构和事件驱动 UI，实现清晰的系统拆分与可扩展架构。

---

# 项目演示

当前版本实现了塔防游戏的核心玩法：

- 敌人沿固定路径移动
- 波次系统生成敌人
- 玩家在指定位置建造防御塔
- 防御塔自动索敌并攻击
- 子弹命中敌人造成伤害
- 敌人死亡获得金币奖励
- 漏怪扣除基地生命
- UI 实时显示 Gold / Lives / Wave
- 胜利与失败状态判定

---

# 技术栈

- Unity 2022.3 LTS
- C#
- Unity 2D Physics
- 组件化开发
- 事件驱动 UI 更新

核心 API：

- Physics2D.OverlapCircleAll
- Vector3.MoveTowards
- Instantiate
- Destroy

---

# 核心系统

## 敌人路径系统

敌人通过 Waypoint 路径移动。

路径由多个节点组成，敌人按顺序移动到每个节点。

核心逻辑：

Vector3.MoveTowards()

支持：

- 多节点路径
- 可扩展多条路线

---

## 波次刷怪系统

WaveSpawner 控制敌人生成逻辑。

每一波可配置：

- Enemy Prefab
- 数量
- 生成间隔
- 速度倍率

系统负责：

- 控制敌人生成
- 波次递增
- 波次结束奖励

---

## 防御塔攻击系统

防御塔会自动搜索攻击范围内的敌人。

实现流程：

1. 扫描范围内敌人
2. 选择目标
3. 发射子弹
4. 子弹追踪目标

核心逻辑：

Physics2D.OverlapCircleAll()

---

## 子弹伤害系统

子弹击中敌人后：

- 触发伤害
- 扣除敌人血量
- 敌人死亡

核心逻辑：

EnemyHealth.TakeDamage()

敌人死亡后：

- 销毁对象
- 奖励金币

---

## 经济系统

游戏包含完整资源循环：

玩家获得金币：

- 击杀敌人
- 波次奖励

玩家消耗金币：

- 建造防御塔

核心逻辑：

Gold -= BuildCost  
Gold += EnemyReward

---

## 基地生命系统

当敌人到达终点：

Lives -= leakDamage

当生命值为 0 时：

Game Over

---

## UI 系统

HUD 显示游戏状态：

- Gold
- Lives
- Wave
- GameOver
- Victory

UI 更新采用 **事件驱动方式**，减少 UI 与游戏逻辑的耦合。

---

# 项目结构

Assets
 ├─ Scripts
 │   ├─ Core
 │   │   └─ GameManager
 │   │
 │   ├─ Pathing
 │   │   └─ WaypointPath
 │   │
 │   ├─ Enemies
 │   │   ├─ EnemyMover
 │   │   └─ EnemyHealth
 │   │
 │   ├─ Combat
 │   │   ├─ Tower
 │   │   └─ Bullet
 │   │
 │   ├─ Spawning
 │   │   └─ WaveSpawner
 │   │
 │   ├─ Building
 │   │   └─ BuildSpot
 │   │
 │   └─ UI
 │       └─ HudView
 │
 ├─ Prefabs
 │
 └─ Scenes

Docs
 └─ UnitySetup.md

---

# 运行项目

1. 使用 Unity Hub 打开项目
2. 推荐 Unity 版本：

Unity 2022.3 LTS

3. 打开场景：

MainScene

4. 点击 **Play**

验证游戏循环：

刷怪 → 建塔 → 攻击 → 击杀 → 金币增长 → UI更新

---

# 项目亮点

该项目展示了以下游戏开发能力：

- 独立实现塔防核心循环
- 模块化游戏系统设计
- 波次刷怪系统
- 防御塔自动索敌逻辑
- 事件驱动 UI 更新
- 清晰的脚本结构划分

适合作为 **Unity / 游戏开发实习项目展示**。

---

# 后续优化方向

计划继续扩展以下内容：

- 塔升级系统
- 多种敌人类型
- AOE 攻击塔
- 减速塔
- ScriptableObject 配置系统
- 对象池优化（减少 Instantiate / Destroy）
- 更完整 UI
- 音效与简单动画

---

# 作者

GitHub  
LCZXXX
