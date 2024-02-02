extends Node
class_name GameController

signal update_score(score_gain: int)


func _ready():
	update_score.connect(_update_score)


func _update_score(score_gain):
	var score_label = get_parent().get_node("ScoreLabel")
	var _score = GlobalValue.score + score_gain
	score_label.text = str(_score)
	GlobalValue.score = _score


func retry():
	get_tree().reload_current_scene()
