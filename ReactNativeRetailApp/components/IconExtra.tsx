import { Component } from 'react';
import * as Font from 'expo-font';
import { createIconSetFromIcoMoon } from '@expo/vector-icons';
import { Icon, IconProps } from 'galio-framework';

import GalioConfig from '../assets/fonts/galioExtra.json';

const GalioExtra = require('../assets/fonts/galioExtra.ttf');
const IconGalioExtra = createIconSetFromIcoMoon(GalioConfig, "GalioExtra", GalioExtra);

type IconExtraProps = IconProps & {
    name: string;
    family?: string;
};

type IconExtraState = {
    fontLoaded: boolean;
};

export default class IconExtra extends Component<IconExtraProps, IconExtraState> {
    state: IconExtraState = {
        fontLoaded: false,
    };

    async componentDidMount() {
        await Font.loadAsync({ GalioExtra });
        this.setState({ fontLoaded: true });
    }

    render() {
        const { name, family, ...rest } = this.props;

        if (name && family && this.state.fontLoaded) {
            if (family === 'Galio') {
                return <IconGalioExtra name={name} {...rest} />;
            }
            return <Icon name={name} family={family} {...rest} />;
        }

        return null;
    }
}
