let assetsManager;

export const clearTexture = (object, scene, childName) => {
    if (object.material) {
        object.material.dispose(true, true);
        object.material = null;
    }
    if (!object.material) {
        let name = childName ? childName : object.name;
        object.material = new BABYLON.StandardMaterial(`${name}_material`, scene);
        object.material.diffuseColor = new BABYLON.Color4(1, 1, 1, 1);
    }
}


export const clearTextures  = (object, scene) => {
    clearTexture(object, scene);

    if (object.getChildMeshes()) {
        object.getChildMeshes().forEach((child, index) => {
            let name = `${object.name}_${index}_${child.name}_material`;
            clearTextures(child, scene, name);
        });
    }
}



export const changeTexture = (object, texture, scene) => {
    if (!object.material) {
        changeColorRGBA(object, { r: 255, g: 255, b: 255, a: 255 }, scene)
    }
    if (object.material.diffuseTexture) {
        object.material.diffuseTexture.dispose();
        object.material.diffuseTexture = null;
    }

    object.material.diffuseTexture = new BABYLON.Texture(texture, scene);
    if (object.getChildMeshes()) {
        object.getChildMeshes().forEach((child, index) => {
            if (child.material.diffuseTexture) {
                child.material.diffuseTexture.dispose();
                child.material.diffuseTexture = null;
            }
            if (!child.material) {
                child.material = new BABYLON.StandardMaterial(`${object.name}_${index}_${child.name}_material`, scene);
            }

            child.material.diffuseTexture = new BABYLON.Texture(texture, scene);
        });
    }
}

export const changeColorRGBA = (object, color, scene) => {
    let c = {
        r: color.r > 0 ? color.r / 255 : 0,
        g: color.g > 0 ? color.g / 255 : 0,
        b: color.b > 0 ? color.b / 255 : 0,
        a: color.a > 0 ? color.a / 255 : 0,
    }

    if (!object.material) {
        object.material = new BABYLON.StandardMaterial(`${object.name}_material`, scene);
    }

    object.material.diffuseColor = new BABYLON.Color4(c.r, c.g, c.b, c.a);
    object.material.alpha = c.a;

    if (object.getChildMeshes()) {
        object.getChildMeshes().forEach((child, index) => {
            if (!child.material) {
                child.material = new BABYLON.StandardMaterial(`${object.name}_${index}_${child.name}_material`, scene);
            }
            child.material.diffuseColor = new BABYLON.Color4(c.r, c.g, c.b, c.a);
            child.material.alpha = c.a;
        });
    }
}





export const sumVector3 = (arr) => {
    let sum = { x: 0, y: 0, z: 0 };

    arr.forEach((data) => {
        sum.x += data.x;
        sum.y += data.y;
        sum.z += data.z;
    });

    return sum;
};

export const degreesToRadians = (degrees) => {
    if (degrees == 0) return 0;
    return (degrees * Math.PI) / 180;
};

export const radiansToDegrees = (radians) => {
    if (radians == 0) return 0;
    return (radians * 180) / Math.PI;
};

export const createScene = (engine) => {
    var scene = new BABYLON.Scene(engine);
    return scene;
};

export const toggleVisibility = (object, state) => {
    object.isVisible = state;
    if (object.getChildMeshes()) {
        object.getChildMeshes().forEach((child) => {
            toggleVisibility(child, state);
        });
    }
};

export const showObject = (object) => {
    toggleVisibility(object, true);
};

export const hideObject = (object) => {
    toggleVisibility(object, false);
};

export const setAssetManager = (scene) => {
    return new BABYLON.AssetsManager(scene);
};

export const setMainLight = (scene) => {
    return new BABYLON.HemisphericLight(
        "lightMain",
        new BABYLON.Vector3(0, 1, 0),
        scene
    );
};

export const setMainGround = (scene, size) => {
    size = size || { x: 10, y: 10, z: 1 };
    let ground = BABYLON.Mesh.CreateGround(
        "groundMain",
        size.x,
        size.y,
        size.z,
        scene
    );
    ground.position.y = 0;

    return ground;
};

export const mainCamera = () => {
    let _camera;

    const create = (scene, canvas) => {
        var cameraPosition = new BABYLON.Vector3.Zero(); //new BABYLON.Vector3(0, 0, 10);
        var startPosition = new BABYLON.Vector3(150, Math.PI / 3, 10 * 110);

        _camera = new BABYLON.ArcRotateCamera(
            "CameraMain",
            startPosition.x,
            startPosition.y,
            startPosition.z,
            cameraPosition,
            scene
        );

        _camera = attachToCanvas(_camera, canvas);

        return _camera;
    };

    const attachToCanvas = (camera, canvas) => {
        camera.attachControl(canvas, false);

        // camera.attachControl(canvas, true);
        // camera.keyUp.push(87);
        // camera.keyDown.push(83);
        // camera.keyLeft.push(65);
        // camera.keyRight.push(68);

        return camera;
    };

    const setTarget = (camera, target) => {
        camera.setTarget(target);
    };

    const setTargetToZero = (camera) => {
        camera.setTarget(BABYLON.Vector3.Zero());
    };

    return {
        create: create,
        setTarget: setTarget,
        setTargetToZero: setTargetToZero,
    };
};

export const AddMesh = async (data, out) => {
    /*
    Data Model
    name,
    scene,
    asset,
    visible,
    onSuccess,
    onError,
    */

    try {

        let name = data.name;
        let scene = data.scene;
        let asset = data.asset;
        let onSuccess = data.onSuccess;
        let onError = data.onError;
        let visible = data.visible;
        let d = data.data;

        assetsManager = assetsManager || setAssetManager(scene);

        out["isVisible"] = true;

        let result = assetsManager.addMeshTask(
            name,
            "",
            asset.folder,
            asset.file
        );

        result.onSuccess = function (task) {
            // out.addChild(task.loadedMeshes[0]);

            for (let i = 0; i < task.loadedMeshes.length; i++) {
                task.loadedMeshes[i].parent = out;
            }
            out.scaling = new BABYLON.Vector3(
                asset.scaling.x,
                asset.scaling.y,
                asset.scaling.z
            );
            out.position = new BABYLON.Vector3(
                asset.position.x,
                asset.position.y,
                asset.position.z
            );
            out.rotation = new BABYLON.Vector3(
                degreesToRadians(asset.rotation.x),
                degreesToRadians(asset.rotation.y),
                degreesToRadians(asset.rotation.z),
            );

            if(d.applyMaterial){
                // console.log('applyMaterial', d.applyMaterial);
                // changeTexture(out, d.applyMaterial, scene);
            }


            if(d.applyColor){
                // console.log('applyColor', d.applyColor);
                changeColorRGBA(out, d.applyColor, scene);
            }


            if (onSuccess && typeof onSuccess === "function") {
                onSuccess();
            }
        };

        out.onError = function (task, message, exception) {
            console.log(message, exception);

            if (onError && typeof onError === "function") {
                onError();
            }
        };

        // if (asset.textures) {
        //     for (let i = 0; i < asset.textures.length; i++) {
        //         const texture = asset.textures[i];

        //         out["textures_tasks"][i] =
        //             assetsManager.addTextureTask(texture.name, texture.image);
        //         out["textures_tasks"][i].onSuccess = function (
        //             task
        //         ) {
        //             material.diffuseTexture = task.texture;
        //         };
        //     }
        // }

        await assetsManager.loadAsync();

        if (visible === false) {
            hideObject(out);
        }
    } catch (error) {
        console.log(error, data);
    }

};