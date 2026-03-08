# Tower Defense 配置核查清单

这份清单用于确认“功能已经写了，但场景里有没有配好”。

## 1. 金币系统（你关心的重点）
金币系统已经在代码里实现，核心脚本是 `GameManager + BuildSpot + EnemyHealth + WaveSpawner + HudView`。

你需要确认这些字段：
- `GameManager.startGold`：初始金币
- `BuildSpot.buildCost`：建塔花费
- `EnemyHealth.rewardGold`：击杀奖励
- `WaveSpawner.clearBonusGold`：通关奖励
- `HudView.goldText`：金币 UI 文本绑定

判定标准：
- 点击建塔点后，金币减少
- 击杀敌人后，金币增加
- 波次全部完成后，金币增加 `clearBonusGold`

## 2. 生命与失败
确认：
- `GameManager.startLives`
- `EnemyMover.leakDamage`
- `HudView.livesText`
- `HudView.statusText`

判定标准：
- 漏怪后 Lives 减少
- Lives 到 0 显示 `GAME OVER`

## 3. 波次与刷怪
确认：
- `WaveSpawner.waves` 每一波都填了 `enemyPrefab/count/startDelay/spawnInterval/speedMultiplier`
- `WaveSpawner.spawnPoint` 已绑定
- `WaveSpawner.path` 已绑定
- `HudView.waveText` 已绑定

判定标准：
- 波次数字递增
- 每波都能正常刷怪

## 4. 建塔基础
确认：
- 每个 BuildSpot 都有 `Collider2D`
- `BuildSpot.towerPrefab` 已绑定
- `BuildSpot.blockWhenPointerOverUI`：建议先关掉，排查完成后再决定是否开启

判定标准：
- 点击可建塔点可以生成塔
- 已建塔点不能重复建塔（当 `disableSpotAfterBuild` 开启）

## 5. 塔攻击链路
确认：
- 塔有 `Tower` 脚本，且 `bulletPrefab` 已绑定
- `Tower.enemyLayers` 包含敌人 Layer
- 敌人有 `Collider2D + EnemyHealth`
- 子弹有 `Bullet` 脚本

判定标准：
- 敌人进范围后开火
- 命中后敌人掉血并死亡

## 6. UI 绑定
确认：
- `HudView` 挂在场景对象上
- `goldText/livesText/waveText/statusText` 全绑定
- `waveSpawner` 已绑定

判定标准：
- Gold/Lives/Wave 文本实时变化
- 胜利后显示 `VICTORY`

## 7. 当前阶段优化路线（按优先级）
1. 玩法优化：
   - 塔升级（伤害/射速/范围）
   - 2~3 种敌人（快、硬、护盾）
   - 新塔类型（单体高伤、AOE、减速）
2. 代码优化：
   - 用 ScriptableObject 管塔/敌人配置
   - 增加状态机（`Playing/Paused/GameOver/Victory`）
   - 事件统一管理，减少对象间硬引用
3. 性能优化：
   - 子弹与敌人对象池（替代频繁 Instantiate/Destroy）
   - 减少每帧分配和 `OverlapCircleAll` 开销
   - 合理使用 Layer 与碰撞矩阵

