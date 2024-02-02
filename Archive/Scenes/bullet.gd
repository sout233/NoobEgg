extends Area2D

const SPEED = 2000.0
var area_direction = Vector2(0, 0)
var debounce = false

func _process(delta):
	self.translate(area_direction * SPEED * delta)

func _on_body_entered(body:Node2D):
	pass # Replace with function body.
