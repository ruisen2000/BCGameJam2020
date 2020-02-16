extends Node2D


const IDLE_DURATION = 1.0

export var move_to = Vector2.RIGHT * 500
export var speed = 3.0

onready var platform = $Platform 
onready var tween = $MoveTween


# Called when the node enters the scene tree for the first time.
func _ready():
	_init_tween()


func _init_tween():
	var duration = move_to.length() / float(speed * Globals.UNIT_SIZE)
	tween.interpolate_property(platform, "position", Vector2.ZERO, move_to, duration, Tween.TRANS_LINEAR, Tween.EASE_IN_OUT, IDLE_DURATION)
	tween.interpolate_property(platform, "position", move_to, Vector2.ZERO, duration, Tween.TRANS_LINEAR, Tween.EASE_IN_OUT, duration + IDLE_DURATION * 2)
	tween.start()
