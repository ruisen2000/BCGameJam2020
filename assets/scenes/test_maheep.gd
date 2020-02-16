extends Node2D


# Declare member variables here. Examples:
# var a = 2
# var b = "text"

var p1
var p2
var rope_
# Called when the node enters the scene tree for the first time.
func _ready():
	p1 = get_node("KinematicBody2D")
	p2 = get_node("KinematicBody2D2")
	rope_ = get_node("rope__") # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	rope_.clear_points()
	rope_.add_point(p1.position)
	rope_.add_point(p2.position)

	
