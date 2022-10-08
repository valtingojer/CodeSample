<template>
  <div class="babylon">
    <h1 style="position: absolute; z-index: 9">Babylon page</h1>
    <babylon-inspector
      :hide-transform="false"
      :loadAssets="assetsToLoad"
      :loadShapes="shapesToLoad"
      :exchangeable-assets="exchangeableAssets"
      :textureable-assets="textureableAssets"
    />
    <div class="babylon-container">
      <babylon-render
        :id="config.canvasId"
        :assets="assets"
        :loadAssets="assetsToLoad"
        :loadShapes="shapesToLoad"
      />
    </div>
  </div>
</template>

<style>
.position-input {
  width: 70px;
}

.babylon {
  height: 100%;
  width: 100%;
  /* border: 1px solid blue; */
  overflow: hidden;
}

.babylon-container {
  width: 100%;
  height: calc(100vh - 96px);
}

hr {
  margin: 12px 0 18px 0;
}
</style>

<script>
import { Assets, AssetsToLoad, ShapesToLoad, ExchangeableAssets, TextureableAssets } from "@/dataFiles/BabylonPage.js";
import * as BabylonHelper from "@/helpers/BabylonHelper.js";

import BabylonRender from "@/components/BabylonRender.vue";
import BabylonInspector from "@/components/BabylonInspector.vue";

export default {
  components: {
    BabylonRender,
    BabylonInspector,
  },
  data() {
    return {
      assets: Assets,
      assetsToLoad: AssetsToLoad,
      shapesToLoad: ShapesToLoad,
      exchangeableAssets: ExchangeableAssets,
      textureableAssets: TextureableAssets,
      config: {
        canvasId: "canvas",
      },
    };
  },
  //   computed: {},
  // methods: {},
  // beforeCreate() {},
  // created() {},
  // beforeMount() {},
  // mounted() {},
  // watch:{}
  // beforeUpdate() {},
  // updated() {},
  // activated() {},
  // deactivated() {},
  // beforeUnmount() {},
  // unmounted() {},
  // errorCaptured() {},
  // renderTracked() {},
  // renderTriggered() {},
};
</script>
