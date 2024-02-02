extends Marker2D

@export var Enemy: PackedScene

@export var min_spawn_interval: float = 1
@export var max_spawn_interval: float = 5

@export var spawn_animation:bool = true

var tilemap: TileMap


func _ready():
	randomize()
	tilemap = get_parent().get_parent().get_node("TileMap")
	$Timer.set_wait_time(randf_range(1, 5))
	$Timer.start()
	print(tilemap.get_used_rect().end.x * tilemap.tile_set.tile_size.x * 6)


func spawn() -> void:
	randomize()

	if spawn_animation:
		get_node("AnimationPlayer").play("spawn")

	var _temp = Enemy.instantiate()
	_temp.global_position = self.global_position + Vector2(randf_range(-2, 2), randf_range(-2, 2))
	get_tree().current_scene.add_child(_temp)

	$Timer.set_wait_time(randf_range(min_spawn_interval, max_spawn_interval))

	var used = tilemap.get_used_rect()
	var tile_size = tilemap.tile_set.tile_size * 6
	position = Vector2(
		randf_range(
			used.position.x * tile_size.x + tile_size.x, used.end.x * tile_size.x - tile_size.x
		),
		randf_range(
			used.position.y * tile_size.y + tile_size.y, used.end.y * tile_size.y - tile_size.y
		)
	)
