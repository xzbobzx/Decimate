import bpy

bpy.ops.object.select_all(action='SELECT')

sel = bpy.context.selected_objects
#act = bpy.context.active_object

for obj in sel:
    if obj.type in 'MESH':
        bpy.context.scene.objects.active = obj #sets the obj accessible to bpy.ops    
        bpy.ops.object.modifier_add(type='DECIMATE')
        bpy.context.object.modifiers["Decimate"].ratio = 0.1
        str1 = bpy.context.object.name
        str2 = "_LOD1"
        bpy.context.object.name = str1 + str2

#bpy.context.scene.objects.active = act

bpy.ops.object.select_all(action='DESELECT')