extends CharacterBody2D

@export_group("Stats")
@export var speed: float = 200.0

@onready var killed_particles: CPUParticles2D = $KilledParticles

var player: CharacterBody2D


func _ready():
	player = get_parent().get_node("Player")


func _process(delta):
	if player != null:
		self.velocity = Vector2(0, 0)
		self.position.x = move_toward(self.position.x, player.get("position").x, speed * delta)
		self.position.y = move_toward(self.position.y, player.get("position").y, speed * delta)

	move_and_slide()


func die_callback(attack: Attack):
	killed_particles.gravity = attack.attack_direction * 100
	killed_particles.set_emitting(true)
	killed_particles.reparent(get_parent().get_parent())

	$AudioStreamPlayer2D.play()
	$AudioStreamPlayer2D.reparent(get_parent().get_parent())

	get_tree().current_scene.get_node("GameController").update_score.emit(1)
	self.queue_free()
