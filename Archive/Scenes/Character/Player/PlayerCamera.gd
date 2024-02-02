## COPY FROM AV228909758
extends Camera2D

@export var dynamic_enabled: bool = false
@export_range(0, 1, 0.01) var dynamic_factor: float = 0.5

var target = null
var objects = null
@onready var max_dist: float = (get_viewport_rect().size / 2).distance_to(get_viewport_rect().size)
@onready var normal_zoom = zoom
@onready var normal_position = position
@onready var normal_smoothing_speed = position_smoothing_speed

var _duaration: float = 0
var _period_in_ms: float = 0
var _amplitude: float = 0
var _timer: float = 0

var _previous_x: float = 0
var _previous_y: float = 0

var _last_shook_timer = 0
var _last_offset := Vector2(0, 0)


func _process(delta):
	if target:
		follow_target(delta)

	if _timer == 0:
		return

	_last_shook_timer = _last_shook_timer + delta
	while _last_shook_timer >= _period_in_ms:
		_last_shook_timer = _last_shook_timer - _period_in_ms
		var intensity = _amplitude * (1 - ((_duaration - _timer) / _duaration))
		var new_x = randf_range(-1.0, 1.0)
		var x_component = intensity * (_previous_x + (delta * (new_x - _previous_x)))
		var new_y = randf_range(-1.0, 1.0)
		var y_component = intensity * (_previous_x + (delta * (new_x - _previous_x)))
		_previous_x = new_x
		_previous_y = new_y
		var new_offset = Vector2(x_component, y_component)
		set_offset(get_offset() - _last_offset + new_offset)
		_last_offset = new_offset

	_timer = _timer - delta
	if _timer <= 0:
		_timer = 0
		set_offset(get_offset() - _last_offset)


func shake(duration, frequency, amplitude) -> void:
	if frequency == 0:
		return

	_duaration = duration
	_timer = duration
	_period_in_ms = 1.0 / frequency
	_amplitude = amplitude
	_previous_x = randf_range(-1.0, 1.0)
	_previous_y = randf_range(-1.0, 1.0)
	set_offset(get_offset() - _last_offset)
	_last_offset = Vector2(0, 0)


func follow_target(delta: float) -> void:
	global_position = lerp(global_position, target.global_position, 12.0 * delta)


func reset_camera() -> void:
	global_position = normal_position
	zoom = normal_zoom


func start_tracking(new_target) -> void:
	target = new_target
	$Tween.remove_all()
	$Tween.interpolate_property(
		self, "zoom", zoom, Vector2(0.8, 0.8), 0.8, Tween.TRANS_CUBIC, Tween.EASE_IN_OUT
	)
