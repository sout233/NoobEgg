extends Node2D

func _ready():
	Input.set_mouse_mode(Input.MOUSE_MODE_HIDDEN)


func _process(_delta):
	var tween = get_tree().create_tween()
	var new_position = get_global_mouse_position()
	
	var direction_x = Input.get_axis("Left", "Right")
	var direction_y = Input.get_axis("Up", "Down")
	if direction_x == 0 and direction_y == 0:
		tween.tween_property(self, "position", new_position, 0.1)
	else:
		tween.tween_property(self, "position", new_position, 0.2)

