extends Area2D


# Declare member variables here. Examples:
# var a = 2
# var b = "text"

export(NodePath) var p1
export(NodePath) var p2

var p1_
var p2_
# Called when the node enters the scene tree for the first time.
func _ready():
	p1_ = get_node(p1)
	p2_ = get_node(p2)
	


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if overlaps_body(p1_) == true || overlaps_body(p2_) ==true :
		get_tree().change_scene("res://assets/scenes/final_cutscene.tscn")
