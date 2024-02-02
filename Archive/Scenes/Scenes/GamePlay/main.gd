extends Node2D

@onready var tilemap: TileMap = $TileMap
@onready var camera: Camera2D = $Player/PlayerCamera/CameraAnchor/Camera


func _ready():
	prepare_tile()


func clamp_camera() -> void:
	var used := tilemap.get_used_rect()
	var tile_size := tilemap.tile_set.tile_size

	camera.limit_top = used.position.y * tile_size.y
	camera.limit_left = used.position.x * tile_size.x
	camera.limit_bottom = used.end.y * tile_size.y
	camera.limit_right = used.end.x * tile_size.x


func prepare_tile() -> void:
	tilemap.clear_layer(1)
	set_random_tile(1, Vector2i(5, 1), 40)
	set_random_tile(1, Vector2i(5, 2), 2)
	set_random_tile(1, Vector2i(6, 2), 2)
	set_random_tile(1, Vector2i(9, 3), 4)


func set_random_tile(layer, source_pos, prop) -> void:
	randomize()
	var bg1_cells = tilemap.get_used_cells(0)
	for cell_pos in bg1_cells:
		if randi_range(0, 1000) <= prop:
			tilemap.set_cell(layer, cell_pos, 0, source_pos)
