<template>
        <div class="accordion-item">
            <h2 class="accordion-header" @click="isOpen=!isOpen">
                <button class="accordion-button" :class="{ 'collapsed': !isOpen }" type="button">
                    {{header}}
                </button>
            </h2>
            <div class="accordion-collapse collapse show" aria-labelledby="headingOne">
                <div class="accordion-body" v-show="isOpen">
                    <slot />
                </div>
            </div>
        </div>
</template>
<script setup lang="ts">
    import { ref } from 'vue';
     defineProps({
        header: {
            type: String,
         },
    });
    const isOpen = ref(false);
</script>
