# Unity 2D Tower Defense (Intern Project Base)

一个从零搭建的 Unity 2D 塔防基础项目，目标是作为实习简历中的可展示作品。

## 技术栈
- Unity 2022.3 LTS (推荐)
- C#
- 2D Physics (`Physics2D.OverlapCircleAll`)
- 事件驱动 UI 更新（资源/生命/波次）

## 当前已实现
- 敌人沿固定路径移动（`Vector3.MoveTowards`）
- 波次刷怪系统（可配置波次/间隔/速度倍率）
- 防御塔索敌与开火
- 子弹追踪与伤害结算
- 金币系统（建塔消耗 + 击杀奖励）
- 基地生命系统（漏怪扣血）
- 基础 HUD（Gold/Lives/Wave/GameOver/Victory）

## 目录结构
```text
Assets/Scripts
  Core
  Pathing
  Enemies
  Combat
  Spawning
  Building
  UI
Docs
```

## 快速开始
1. 用 Unity Hub 创建一个 2D 项目，把本仓库内容放入项目根目录。
2. 按 [UnitySetup.md](Docs/UnitySetup.md) 配置场景对象和 Prefab 引用。
3. 点击 Play，验证“刷怪 -> 建塔 -> 攻击 -> 结算 -> UI 更新”闭环。

## 简历可写亮点
- 独立搭建塔防核心循环：路径系统、战斗系统、波次系统、经济系统。
- 使用事件驱动解耦 UI 与游戏逻辑，降低模块耦合度。
- 代码按领域拆包，便于后续扩展（升级塔、技能塔、Boss、存档等）。

## 下一阶段建议
- 塔升级系统（数值成长 + 视觉反馈）
- 不同敌人类型（高血量/高速/护盾）
- ScriptableObject 配置化（塔和敌人数据）
- 对象池（优化 Instantiate/Destroy）

