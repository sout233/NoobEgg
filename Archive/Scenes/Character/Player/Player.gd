extends CharacterBody2D

@export var Bullet: PackedScene
@onready var Camera = $"PlayerCamera/CameraAnchor/Camera"
@onready var CameraAnchor = $"PlayerCamera/CameraAnchor"
@onready var HealthComp: Node = $"HealthComp"
@onready var GameHud: CanvasLayer = get_tree().current_scene.get_node("GameHUD")
@onready var HealthBar := GameHud.get_node("HealthBar")
@onready var DamageScreen := GameHud.get_node("DamageScreen")
@onready var AmmorLabel: Label = GameHud.get_node("AmmorLabel")
@onready var ThisSprite: AnimatedSprite2D = get_node("AnimatedSprite2D")
@onready var Wp01 = get_node("WP01")  #TODO: DELETE

const DEFAULT_SPEED: float = 700.0
const FIRE_SPEED: float = 500.0
var speed: float

var shoot_timer: float = 0

@export var fire_rate: float = 0.1
var actual_rate: float = 0.1

var ammor = 90


func _ready():
	Camera.set("position", Vector2(40, 0))
	speed = DEFAULT_SPEED
	AmmorLabel.text = str(ammor)


func _physics_process(delta):
	shoot_timer += delta

	if Input.get_action_raw_strength("Shoot") && shoot_timer >= actual_rate:
		if ammor > 0:
			var _bullet = Bullet.instantiate()
			add_sibling(_bullet)
			_bullet.global_position = Wp01.get_node("MuzzleMarker").global_position
			_bullet.set(
				"area_direction", (get_global_mouse_position() - self.global_position).normalized()
			)
			Camera.shake(0.5, 20, 10)
			shoot_timer = 0
			speed = FIRE_SPEED
			ammor -= 1
			AmmorLabel.text = str(ammor)
		else:
			$AudioStreamPlayer.play()
			shoot_timer = 0
			AmmorLabel.text = "0"
	else:
		Camera.set("offset", Vector2(0, 0))
		speed = DEFAULT_SPEED

	var direction_x = Input.get_axis("Left", "Right")
	var direction_y = Input.get_axis("Up", "Down")
	velocity.x = 0
	velocity.y = 0

	Camera.set("offset", Vector2(0, 0))

	if direction_x:
		velocity.x = direction_x * speed
	if direction_y:
		velocity.y = direction_y * speed

	CameraAnchor.look_at(get_global_mouse_position())
	Wp01.look_at(get_global_mouse_position())
	if get_global_mouse_position().x < position.x:
		ThisSprite.flip_h = true
		Wp01.get_node("Wp01").flip_v = true
		Wp01.position.x = -6.0
	else:
		ThisSprite.flip_h = false
		Wp01.get_node("Wp01").flip_v = false
		Wp01.position.x = 6.0

	move_and_slide()


func damage_callback(attack: Attack):
	if attack.attack_damage > 0:
		$AnimationPlayer.play("damage")
		DamageScreen.get_node("AnimationPlayer").play("damage_screen")
		update_healthbar()


func die_callback(_attack: Attack):
	HealthBar.value = 0
	var gg_canvas: CanvasLayer = get_tree().current_scene.get_node("GameOverCanvas")
	gg_canvas.visible = true


func update_healthbar():
	HealthBar.value = HealthComp.value


func update_ammorlabel():
	AmmorLabel.text = str(ammor)
