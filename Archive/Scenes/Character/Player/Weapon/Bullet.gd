extends Area2D

@onready var BulletSoundPlayer: AudioStreamPlayer2D = $"BulletSoundPlayer"

const SPEED: float = 2000.0
const ATTACK_DAMAGE: float = 10.0
const KNOCKBACK_FORCE: float = 100.0
var area_direction: Vector2 = Vector2(0, 0)


func _ready():
	# 不知道有什么用，但看起来好看点
	self.get_node("BulletParticles").gravity = area_direction
	BulletSoundPlayer.play()


func _process(delta):
	self.translate(area_direction * SPEED * delta)


# FIXME: 需要修改
# 仅处理Enemy Area2D
func _on_area_entered(area):
	var body = area.get_parent()

	if area is HitboxComp and body.is_in_group("Enemy"):
		var hitbox: HitboxComp = area
		var attack = Attack.new()
		attack.attack_damage = ATTACK_DAMAGE
		attack.knockback_force = KNOCKBACK_FORCE
		attack.attack_direction = area_direction
		hitbox.damage(attack)
		free_bullet()


# 仅处理墙体碰撞体
func _on_body_entered(body):
	if not body.is_in_group("Player"):
		free_bullet()


func free_bullet():
	if self.has_node("BulletParticles"):
		self.get_node("BulletParticles/Timer").start()
		self.get_node("BulletParticles").reparent(get_parent().get_parent())
	if self.has_node("BulletSoundPlayer"):
		BulletSoundPlayer.reparent(get_tree().current_scene)
	queue_free()
