extends Node
class_name HealthComp

@export var show_top_health_bar: bool = true

@export var MAX_HEALTH: float = 100.0

var value: float:
	set(h):
		value = h
		value = clampf(h, 0, 100)


func _ready() -> void:
	value = MAX_HEALTH


func damage(attack: Attack):
	print(1)
	value -= attack.attack_damage

	if value <= 0:
		if get_parent().has_method("die_callback"):
			get_parent().die_callback(attack)
		get_parent().queue_free()
	else:
		if get_parent().has_method("damage_callback"):
			get_parent().damage_callback(attack)
