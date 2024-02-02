extends Area2D
class_name HitboxComp

@export var health_comp: HealthComp
@export var aim_groups: Array

@export_group("Self Attack")
@export var self_damage_amount: float

var self_attack: Attack


func _ready() -> void:
	self_attack = Attack.new()
	self_attack.attack_damage = self_damage_amount


func damage(attack: Attack) -> void:
	if health_comp:
		health_comp.damage(attack)


func _on_area_entered(area):
	if area is HitboxComp:
		var body = area.get_parent()
		for group in aim_groups:
			if body.is_in_group(group) and body.has_node("HitboxComp"):
				damage(body.get_node("HitboxComp").self_attack)
