extends Node2D


func _on_area_2d_body_entered(body:Node2D):
	if body.is_in_group("Player"):
		body.get_node("HealthComp").value+=20
		body.update_healthbar()
		queue_free()
