# Unity 场景搭建步骤（可直接照做）

## 1. 创建场景基础对象
在 Hierarchy 中创建：
- `GameManager`（挂 `GameManager.cs`）
- `Spawner`（挂 `WaveSpawner.cs`）
- `Path`（挂 `WaypointPath.cs`，下面放路径点）
- `BuildSpots`（空物体，用来放建塔点）
- `Canvas` + `EventSystem`（HUD）

## 2. 配置路径点
1. 在 `Path` 下创建子物体：`P0`, `P1`, `P2`, ...（顺序就是路径顺序）。
2. `WaypointPath` 会在 `OnValidate` 自动读取子节点为路径点。
3. 敌人从 `P0` 出发，最终到最后一个点触发漏怪扣血。

## 3. 制作 Enemy Prefab
建议新建一个 `Enemy`（Sprite + `CircleCollider2D`）。
挂载脚本：
- `EnemyHealth`
- `EnemyMover`

建议参数：
- `EnemyHealth.maxHealth = 5`
- `EnemyHealth.rewardGold = 10`
- `EnemyMover.moveSpeed = 2`
- `EnemyMover.leakDamage = 1`

注意：
- Enemy 要有 `Collider2D`，塔索敌依赖 2D 碰撞查询。
- 把 Enemy 设为 `Enemy` Layer，后续给塔的 `enemyLayers` 过滤使用。

## 4. 制作 Bullet Prefab
新建 `Bullet`（小圆点 Sprite 即可），挂 `Bullet.cs`。
建议参数：
- `moveSpeed = 10`
- `hitRadius = 0.1`
- `lifeTime = 3`

## 5. 制作 Tower Prefab
新建 `Tower`（任意 Sprite），挂 `Tower.cs`。
配置：
- `bulletPrefab` 拖入上一步 Bullet Prefab
- `enemyLayers` 选择 `Enemy`
- 可选添加一个子物体 `Muzzle`，拖给 `muzzle`

建议参数：
- `attackRange = 2.5`
- `fireRate = 1`
- `damage = 1`

## 6. 配置建塔点
创建多个空物体（或地块）作为建塔点，挂：
- `BuildSpot.cs`
- `Collider2D`（必须）

配置：
- `towerPrefab` 拖入 Tower Prefab
- `buildCost` 例如 50

运行时点击建塔点，会尝试扣金币并生成塔。

## 7. 配置波次刷怪
在 `Spawner` 上配置：
- `spawnPoint`：创建 `SpawnPoint` 空物体作为出生位置
- `path`：拖入 `Path`
- `waves`：添加多组波次

每个 `WaveDefinition` 至少配置：
- `enemyPrefab`
- `count`
- `startDelay`
- `spawnInterval`
- `speedMultiplier`

## 8. 配置 UI
Canvas 下创建四个 TextMeshPro 文本：
- `GoldText`
- `LivesText`
- `WaveText`
- `StatusText`

创建空物体 `HUD`，挂 `HudView.cs`，把四个文本拖入对应字段。
再把 `Spawner` 拖入 `waveSpawner` 字段。

## 9. 运行检查清单
- 敌人是否按路径移动
- 到达终点是否扣 Lives
- 击杀是否加 Gold
- 点击建塔点是否花费 Gold 并生成塔
- 塔是否自动索敌并发射子弹
- 波次和状态文本是否正确更新

## 10. 常见问题
- 敌人不动：检查 `WaveSpawner.path` 和 `EnemyMover.path` 是否被设置。
- 塔不攻击：检查 Enemy 是否有 `Collider2D`，Layer 是否包含在 `enemyLayers`。
- 点击建塔无效：检查建塔点是否有 `Collider2D`，以及金币是否足够。
- 点击建塔无效：在 `BuildSpot` 中把 `blockWhenPointerOverUI` 先设为 `false`，并观察 Console 的 `[BuildSpot]` 日志定位具体被哪条分支拦截。
- UI 无更新：检查 `HudView` 是否正确绑定 `GameManager` 和各 `TMP_Text`。
