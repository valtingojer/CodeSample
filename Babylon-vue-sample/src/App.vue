<script setup>
import { RouterLink, RouterView } from "vue-router";
import HelloWorld from "./components/HelloWorld.vue";
</script>

<template>
  <div>
    <table class="header">
      <tr>
        <td>
          <img
            alt="Vue logo"
            class="logo"
            src="@/assets/logo.svg"
            width="60"
            height="60"
          />
        </td>
        <td>
          <nav class="menu-nav">
            <!-- <RouterLink to="/">Home</RouterLink> -->
            <!-- <RouterLink to="/about">About</RouterLink> -->
            <RouterLink to="/babylon">3D View</RouterLink>
          </nav>
        </td>
      </tr>
      <tr>
        <td colspan="2">
          <RouterView />
        </td>
      </tr>
    </table>
  </div>
</template>

<style scoped>
table.header {
  width: 100%;
  height: calc(100vh - 1px);
  /* border: 1px solid red; */
}

nav a.router-link-exact-active {
  color: var(--color-text);
}

nav a.router-link-exact-active:hover {
  background-color: transparent;
}

nav a {
  display: inline-block;
  padding: 0 1rem;
  border-left: 1px solid var(--color-border);
}

nav a:first-of-type {
  border: 0;
}

nav {
    text-align: left;
    margin-left: -1rem;
    font-size: 1rem;
    width: 100%;

    padding: 1rem 0;
    margin-top: 1rem;
  }
</style>
